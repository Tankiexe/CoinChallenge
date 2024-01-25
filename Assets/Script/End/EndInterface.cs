using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class EndInterface : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI score;
    public TextMeshProUGUI kills;
    public TextMeshProUGUI coins;

    public Image fondFondu;
    void Start()
    {
        StartCoroutine(ScreenFadeCorout());
        UpdateTitle();
        UpdateScore();
    }


    void UpdateTitle()
    {
        if (PersistentData.instance.playerBoarded)
        {
            title.text = "Victoire";
        }
        else
        {
            title.text = "Défaite";
        }
    }

    void UpdateScore()
    {
        score.text = "Score : " + PersistentData.instance.score;
        kills.text = "Victimes :" + PersistentData.instance.kills;
        coins.text = "Pièces :" + PersistentData.instance.coinsCollected;
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }


    IEnumerator ScreenFadeCorout()
    {
        float t = 1;

        Color color = fondFondu.color;
        while (t < 1.1f)
        {
            color.a = Mathf.Lerp(0, 1, t);
            fondFondu.color = color;
            t -= Time.deltaTime / 4;

            yield return null;
        }
        yield break;
    }
    
}
