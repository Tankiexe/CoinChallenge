using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;
    public static ScoreUI instance;

    private void Awake()
    {
        instance = this;
    }

    public static void UpdateScore()
    {
        instance.scoreText.text = GameManager.SCORE.ToString();
    }
}
