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

    public Camera camera;

    void Start()
    {
        imageFood = GameObject.Find("UI/Canvas/FoodBar/HaveFood");
    }

    
    void Update()
    {
        if (haveFood)
        {
            imageFood.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;
                    Debug.Log(objectHit);
                }

                HungerBar._hunger += energy;
                energy = 0f;
                haveFood = false;
                
                foodEatAudio.PlayOneShot(foodEatClip[Random.Range(0, foodEatClip.Length)], 0.5f);
                
            }
        }
        else
        {
            imageFood.SetActive(false);
        }
    }
}
