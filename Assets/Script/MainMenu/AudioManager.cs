using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManger : MonoBehaviour
{
    public static AudioManger instance;
    public AudioSource buttonClick;

    public Slider sounds;
    public AudioMixer audioMixer;
    public Slider musicVolume;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void OnSfxValueChange(string mixerGroup)
    {
        float t = sounds.value;
        float db = Mathf.Lerp(-20f, 5f, t);
        audioMixer.SetFloat(mixerGroup, db);

    }

    public void OnMusicValueChange(string mixerGroup)
    {
        float t = musicVolume.value;
        float db = Mathf.Lerp(-20f, 5f, t);
        audioMixer.SetFloat(mixerGroup, db);

    }
    public void ToPlaySound(AudioSource source)
    {
        source.Play();
    }
}
