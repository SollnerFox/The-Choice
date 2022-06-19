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

    void Start()
    {
        imageFood = GameObject.Find("UI/Canvas/FoodBar/HaveFood");
        textOver = GameObject.Find("UI/Canvas/FoxyTextOver");
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
