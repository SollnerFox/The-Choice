using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetHungerBar : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;
    private Slider _hungerBarSlider;
    public static float _hunger;
    private float maxHunger = 100f;

    private void Start()
    {
        _hungerBarSlider = GetComponent<Slider>();
        _hunger = maxHunger;
        _fill.color = _gradient.Evaluate(1f);
    }

    private void Update()
    {
        _hungerBarSlider.value = _hunger;
        _fill.color = _gradient.Evaluate(_hungerBarSlider.normalizedValue);
        _hunger -= 1f * Time.deltaTime;
        if (_hunger > maxHunger) _hunger = maxHunger;
        if (_hunger <= 0f)
        {
            //_deathUiObject.SetActive(true);
        }   
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCamera.forward);
    }
    
}
