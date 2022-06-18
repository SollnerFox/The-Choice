using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSource;
    private float musicVol = 1f;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVol;
    }

    public void SetVolume(float vol)
    {
        musicVol = vol;
    }
}
