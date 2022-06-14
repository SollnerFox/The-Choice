using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [Range(0, 1)]
    public float timeOfDay;
    public float dayDuration = 30f;

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
        if (timeOfDay >= 1) timeOfDay -= 1;

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
