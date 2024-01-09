using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndInterface : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI score;
    void Start()
    {
        UpdateTitle();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void OnRestartClick()
    {

    }


}
