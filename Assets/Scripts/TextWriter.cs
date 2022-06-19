using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    public GameObject dayManagerObject;
    DayNightManager dayManager;

    [SerializeField] 
    Text textDayCurrent;
    public GameObject _dayUIObj;

    public float textDelay=2f;



    // Start is called before the first frame update
    void Start()
    {
        dayManager = dayManagerObject.GetComponent<DayNightManager>();
        StartCoroutine(WriteCurrentDay("Day "+ dayManager.dayCurrent, textDelay));
    }

    public IEnumerator WriteCurrentDay(string txt, float delay)
    {
        _dayUIObj.SetActive(true);
        foreach (var symb in txt)
        {
            textDayCurrent.text += symb;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(2.0f);
        textDayCurrent.text = "";
        _dayUIObj.SetActive(false);
    }
}
