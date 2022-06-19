using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContainer : MonoBehaviour
{
    public bool haveFood;
    public float energy;
    GameObject imageFood;

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
                HungerBar._hunger += energy;
                energy = 0f;
                haveFood = false;
            }
        }
        else
        {
            imageFood.SetActive(false);
        }
    }
}
