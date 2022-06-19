using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [Range(0, 1)]
    public float timeOfDay;
    public float dayDuration = 30f;
    public float dayCurrent=1f;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyboxCurve;

    public Material DaySkybox;
    public Material NightSkybox;

    public ParticleSystem Stars;

    public Light Sun;
    public Light Moon;

    private float sunIntensity;
    private float moonIntensity;

    public GameObject textWriterObj;
    TextWriter textWriter;

    public PauseMenu gameMusic;

    public GameObject[] foodInScene;

    // Start is called before the first frame update
    void Start()
    {
        sunIntensity = Sun.intensity;
        moonIntensity = Moon.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay += Time.deltaTime / dayDuration;
        if (timeOfDay >= 1)
        {
            dayCurrent += 1f;
            timeOfDay -= 1;
            textWriter = textWriterObj.GetComponent<TextWriter>();

            if (dayCurrent == 1)
            {
                gameMusic.gameAudio.clip = gameMusic.dayMusic[0];
                gameMusic.gameAudio.Play();
                foodInScene[0].SetActive(true);
                foodInScene[1].SetActive(false);
                foodInScene[2].SetActive(false);
            }

            if (dayCurrent == 2)
            {
                gameMusic.gameAudio.clip = gameMusic.dayMusic[1];
                gameMusic.gameAudio.Play();
                foodInScene[0].SetActive(false);
                foodInScene[1].SetActive(true);
                foodInScene[2].SetActive(false);
            }

            if (dayCurrent == 3)
            {
                gameMusic.gameAudio.clip = gameMusic.dayMusic[2];
                gameMusic.gameAudio.Play();
                foodInScene[0].SetActive(false);
                foodInScene[1].SetActive(false);
                foodInScene[2].SetActive(true);
            }
            if (dayCurrent == 4)
            {

            }

            StartCoroutine(textWriter.WriteCurrentDay("Day " + dayCurrent, 0.75f));

        }

        RenderSettings.skybox.Lerp(NightSkybox, DaySkybox, SkyboxCurve.Evaluate(timeOfDay));
        RenderSettings.sun = SkyboxCurve.Evaluate(timeOfDay) > 0.1f ? Sun : Moon;
        DynamicGI.UpdateEnvironment();

        var starsMain = Stars.main;
        starsMain.startColor = new Color(1, 1, 1, 1 - SkyboxCurve.Evaluate(timeOfDay));
        
        Sun.transform.localRotation = Quaternion.Euler(timeOfDay * 360f, 180, 0);
        Moon.transform.localRotation = Quaternion.Euler(timeOfDay * 360f + 180f, 180, 0);
        
        Sun.intensity = sunIntensity * SunCurve.Evaluate(timeOfDay);
        Moon.intensity = moonIntensity * MoonCurve.Evaluate(timeOfDay);
    }
}
