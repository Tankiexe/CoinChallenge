using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource buttonClick;
    public AudioSource hit;
    public AudioSource bonk;
    public AudioSource gling;
    public AudioSource music;
    

    public Slider sounds;
    public AudioMixer audioMixer;
    public Slider musicVolume;
    
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
