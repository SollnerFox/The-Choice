using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float energy;
    public bool canBeEaten = true;
    public bool digging = false;

    private void OnTriggerStay(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && canBeEaten)
            {
                HungerBar._hunger += energy;
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (digging && !canBeEaten)
        {
            digging = false;
            canBeEaten = true;
            StartCoroutine(Drop());
        }
    }

    IEnumerator Drop()
    {
        for (var i=0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.035f);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        }

        for (var j = 0; j < 7; j++)
        {
            yield return new WaitForSeconds(0.035f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            transform.Rotate(-15f,0f,0f);
        }

    }
}
