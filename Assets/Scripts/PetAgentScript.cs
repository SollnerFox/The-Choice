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
    public bool playerTooNear, foodIsNear;

    public GameObject foxy;
    Animator _anim;

    Vector3 lastPoint;

    GameObject[] food;
    GameObject closestFood;
    public GameObject nearest;

    public AudioSource voiceAudio;
    public AudioClip[] voiceClip;
    public float voiceTimer = 0.7f;
    private float voiceTimerDown;

    // Start is called before the first frame update
    void Start()
    {
        
        _petAgent = GetComponent<NavMeshAgent>();
        petTransform = GetComponent<Transform>();
        _petAgentPath = new NavMeshPath();

        _anim = foxy.GetComponent<Animator>();

        lastPoint = petTransform.position;


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

            if (playerTooNear && !foodIsNear)
            {
                if (_petAgentPath.status == NavMeshPathStatus.PathComplete)
                {

                    if (Vector3.Distance(petTransform.position, player.position) < distanceToPlayer)
                    {

                        //yield return new WaitForSeconds(3f);
                        nearPlayer.gameObject.SetActive(true);
                        GoToNearRandomPoint();
                    }
                    else
                    {
                        nearPlayer.gameObject.SetActive(false);
                        //target = player;
                    }
                    _petAgent.SetDestination(target.position);
                    yield return new WaitForSeconds(2f);
                }
            }

            if (foodIsNear)
            {
                if (_petAgentPath.status == NavMeshPathStatus.PathComplete)
                {

                    if (Vector3.Distance(petTransform.position, nearest.transform.position) < distanceToFood)
                    {
                        target = nearest.transform;
                        //yield return new WaitForSeconds(3f);

                    }

                    _petAgent.SetDestination(target.position);

                }
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

    void Update()
    {
        playerTooNear = Physics.CheckSphere(petTransform.position, distanceToPlayer, isPlayer);
        foodIsNear = Physics.CheckSphere(petTransform.position, distanceToFood, isFood);
        
        if (foodIsNear)
        {
            nearest = FindClosestFood();
        }

        if (!playerTooNear && !foodIsNear)
        {
            if (lastPoint != petTransform.position)
            {
                _anim.SetBool("Walk", true);
                _anim.SetBool("FindFood", false);
                _anim.SetBool("Dig", false);
                lastPoint = petTransform.position;
            }
            else { _anim.SetBool("Walk", false); }
        }
        

        if (playerTooNear && !foodIsNear)
        {
            if (lastPoint != petTransform.position)
            {
                _anim.SetBool("Walk", true);
                _anim.SetBool("FindFood", false);
                _anim.SetBool("Dig", false);
                lastPoint = petTransform.position;
            }
            else
            {
                _anim.SetBool("Walk", false);
                _anim.SetBool("FindFood", false);
                _anim.SetBool("Dig", false);
                target = player;

                Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.fixedDeltaTime);

                if (voiceTimerDown > 0)
                { voiceTimerDown -= Time.deltaTime; }
                if (voiceTimerDown < 0)
                { voiceTimerDown = 0; }
                if (voiceTimerDown == 0)
                {
                    voiceAudio.PlayOneShot(voiceClip[Random.Range(0, voiceClip.Length)], 0.5f);
                    voiceTimerDown = voiceTimer;
                }
            }

        }

        if (foodIsNear)
        {
            if (lastPoint != petTransform.position)
            {
                _anim.SetBool("Walk", true);
                _anim.SetBool("FindFood", false);
                _anim.SetBool("Dig", false);
                lastPoint = petTransform.position;
            }
            else
            {
                _anim.SetBool("Walk", false);

                if (Vector3.Distance(petTransform.position, nearest.transform.position) < 2.0f)
                {
                    if (!nearest.GetComponent<Food>().canBeEaten)
                    { 
                        _anim.SetBool("Dig", true);
                        StartCoroutine(PetDigging());
                    }
                    else { _anim.SetBool("FindFood", true); }
                }
                target = nearest.transform;
                
                Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.fixedDeltaTime);
            }

        }

    }

    GameObject FindClosestFood()
    {
        float distance = Mathf.Infinity;
        Vector3 position = petTransform.position;
        food = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject go in food)
        {
            Vector3 diff = go.transform.position - position;
            float currDistance = diff.sqrMagnitude;
            if(currDistance < distance)
            {
                closestFood = go;
                distance = currDistance;
            }
        }
        return closestFood;
    }

    IEnumerator PetDigging()
    {
        yield return new WaitForSeconds(3f);
        if (nearest !=null)
        {
            nearest.GetComponent<Food>().digging = true;
            _anim.SetBool("Dig", false);
        }
        
    }

}
