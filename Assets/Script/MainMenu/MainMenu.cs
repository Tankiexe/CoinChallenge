using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnStartClick()
    {
        AudioManager.instance.ToPlaySound(AudioManager.instance.buttonClick);
        SceneManager.LoadScene("Level");
    }

    public void OnQuitClick()
    {
        AudioManager.instance.ToPlaySound(AudioManager.instance.buttonClick);
        Application.Quit();
    }
}
