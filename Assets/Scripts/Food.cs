using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public GameObject berries;
    public float energy;
    public bool canBeEaten = true;
    public bool digging = false;
    bool stayOnFood;

    GameObject textOver;
    FoodContainer playerFood;

    void Start()
    {
        textOver = GameObject.Find("UI/Canvas/FoodTextOver");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && berries != null)
        {
            playerFood = other.GetComponent<FoodContainer>();
            textOver.SetActive(true);
            stayOnFood = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && berries != null)
        {
            playerFood = null;
            textOver.SetActive(false);
            stayOnFood = false;
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

        if (stayOnFood)
        {
            if (Input.GetKeyUp(KeyCode.E) && berries != null)
            {
                
                if (canBeEaten && !playerFood.haveFood)
                {
                    playerFood.haveFood = true;
                    playerFood.energy = energy;
                    playerFood.foodHere = false;
                    //HungerBar._hunger += energy;

                    textOver.SetActive(false);
                    Destroy(berries);
                    berries = null;
                    gameObject.layer = 0;
                    gameObject.tag = "Untagged";
                }
            }
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
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.19f, transform.position.z);
            transform.Rotate(13f,0f,0f);
        }

    }
}
