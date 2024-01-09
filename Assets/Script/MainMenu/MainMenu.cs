using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnStartClick()
    {
        //AudioManger.instance.ToPlaySound(AudioManger.instance.buttonClick);
        SceneManager.LoadScene("Level");
    }

    public void OnQuitClick()
    {
        //AudioManger.instance.ToPlaySound(AudioManger.instance.buttonClick);
        Application.Quit();
    }
}
