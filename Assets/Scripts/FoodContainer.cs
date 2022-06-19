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
    public bool foodHere = false;
    GameObject textOver;
    GameObject textEatFoxy;
    public GameObject foxyObj;
    public GameObject bonesObj;

    public PauseMenu gameMusic;

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
            if (foxyHere && !foodHere && HungerBar._hunger <= 15f)
            {
                textEatFoxy.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(bonesObj, new Vector3(foxyObj.transform.position.x, foxyObj.transform.position.y-0.1f, foxyObj.transform.position.z), foxyObj.transform.rotation);
                    textEatFoxy.SetActive(false);
                    foxyHere = false;
                    Destroy(foxyObj);
                    foxyObj = null;
                    gameMusic.gameAudio.clip = gameMusic.dayMusic[2];
                    gameMusic.gameAudio.Play();
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

        if (other.CompareTag("Food"))
        {
            foodHere = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Foxy"))
        {
            foxyHere = false;
        }

        if (other.CompareTag("Food"))
        {
            foodHere = true;
        }
    }

}
