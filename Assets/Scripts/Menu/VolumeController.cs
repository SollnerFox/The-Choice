using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("volume", volume);
    }
}
