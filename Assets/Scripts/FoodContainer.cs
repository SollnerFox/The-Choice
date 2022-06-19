using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContainer : MonoBehaviour
{
    public bool haveFood;
    public float energy;
    GameObject imageFood;


    public AudioSource foodEatAudio;
    public AudioClip[] foodEatClip;

    public bool foxyHere=false;
    GameObject textOver;
    GameObject textEatFoxy;
    public GameObject foxyObj;
    public GameObject bonesObj;
    [SerializeField] private GameObject _foxDeathPatricle;

    void Start()
    {
        imageFood = GameObject.Find("UI/Canvas/FoodBar/HaveFood");
        textOver = GameObject.Find("UI/Canvas/FoxyTextOver");
        textEatFoxy = GameObject.Find("UI/Canvas/EatFoxyOver");
    }

    
    void FixedUpdate()
    {
        if (haveFood)
        {
            imageFood.SetActive(true);

            if (foxyHere)
            {
                textOver.SetActive(true);
            }
            else
            {
                textOver.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (foxyHere)
                {
                    PetHungerBar._hunger += energy;
                    energy = 0f;
                    haveFood = false;
                }
                else
                {
                    HungerBar._hunger += energy;
                    energy = 0f;
                    haveFood = false;

                    foodEatAudio.PlayOneShot(foodEatClip[Random.Range(0, foodEatClip.Length)], 0.5f);
                }
            }
        }
        else
        {
            imageFood.SetActive(false);
            textOver.SetActive(false);
            if (foxyHere && HungerBar._hunger <= 95f)
            {
                textEatFoxy.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(bonesObj, new Vector3(foxyObj.transform.position.x, foxyObj.transform.position.y-0.1f, foxyObj.transform.position.z), foxyObj.transform.rotation);
                    Instantiate(_foxDeathPatricle, new Vector3(foxyObj.transform.position.x, foxyObj.transform.position.y + 0.2f, foxyObj.transform.position.z), foxyObj.transform.rotation);
                    textEatFoxy.SetActive(false);
                    foxyHere = false;
                    Destroy(foxyObj);
                }
            }
            else { textEatFoxy.SetActive(false); }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Foxy"))
        {
            foxyHere = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Foxy"))
        {
            foxyHere = false;
        }
    }

}
