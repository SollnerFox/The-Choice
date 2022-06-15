using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HungerBar : MonoBehaviour
{
    private Slider _hungerBarSlider;
    public static float _hunger;
    private float maxHunger = 100f;
    
    private void Start()
    {
        _hungerBarSlider = GetComponent<Slider>();
        _hunger = maxHunger;
    }

    
    private void Update()
    {
        _hungerBarSlider.value = _hunger;
        _hunger -= 3.3f * Time.deltaTime;
        if (_hunger > maxHunger) _hunger = maxHunger;
        if (_hunger <= 0f) Debug.Log("U FUCKING DEAD, BITCH!!!");   
    }
}
