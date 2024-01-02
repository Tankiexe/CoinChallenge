using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class AudioLevelManeger : MonoBehaviour
{
    public static AudioLevelManeger Instance;
    public AudioSource hit;
    public AudioSource bonk;
    public AudioSource gling;
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void ToPlaySound(AudioSource source)
    {
        source.Play();
    }
}
