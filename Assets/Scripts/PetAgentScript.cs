using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetAgentScript : MonoBehaviour
{
    NavMeshAgent _petAgent;
    NavMeshPath _petAgentPath;
    //NavMeshHit _petAgentHit;

    Vector3 randomPoint;

    Transform petTransform;
    Transform nearPlayer;
    Transform target;

    public Transform player;
    public GameObject nearPlayerPrefab;
    public LayerMask isPlayer, isFood;

    public float randomPointRadius = 6f;
    public float distanceToPlayer = 20f;
    public float distanceToFood = 20f;
    bool playerTooNear, foodIsNear;
    
    // Start is called before the first frame update
    void Start()
    {
        _petAgent = GetComponent<NavMeshAgent>();
        petTransform = GetComponent<Transform>();
        _petAgentPath = new NavMeshPath();
        target = player;
        nearPlayer = Instantiate(nearPlayerPrefab, Vector3.zero, Quaternion.identity).transform;
        StartCoroutine(PetMoving());
    }

    IEnumerator PetMoving()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            _petAgent.CalculatePath(player.position, _petAgentPath);
            if (!playerTooNear && _petAgentPath.status == NavMeshPathStatus.PathComplete)
            {
                if (Vector3.Distance(petTransform.position, player.position) > randomPointRadius)
                {
                    if (Vector3.Distance(nearPlayer.position, player.position) > randomPointRadius)
                    {
                        nearPlayer.gameObject.SetActive(true);
                        GoToNearRandomPoint();
                    }
                }
                else
                {
                    nearPlayer.gameObject.SetActive(false);
                    //target = player;
                }
                _petAgent.SetDestination(target.position);
            }
        }
    }


    void GoToNearRandomPoint()
    {
        bool getCorrectPoint = false;
        while(!getCorrectPoint)
        {
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(Random.insideUnitSphere * randomPointRadius + player.position, out navMeshHit, randomPointRadius, NavMesh.AllAreas);
            randomPoint = navMeshHit.position;

            if (randomPoint.y > -10000 && randomPoint.y < 10000)
            {
                _petAgent.CalculatePath(randomPoint, _petAgentPath);
                if (_petAgentPath.status == NavMeshPathStatus.PathComplete && !NavMesh.Raycast(player.position, randomPoint, out navMeshHit, NavMesh.AllAreas))
                {
                    getCorrectPoint = true;
                }
            }
        }

        nearPlayer.position = randomPoint;
        target = nearPlayer;
    }

    void FixedUpdate()
    {
        playerTooNear = Physics.CheckSphere(petTransform.position, distanceToPlayer, isPlayer);
        foodIsNear = Physics.CheckSphere(petTransform.position, distanceToFood, isFood);
    }
}
