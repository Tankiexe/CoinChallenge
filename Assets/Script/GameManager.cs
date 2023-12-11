using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    int timeToComplete = 120;
    float currentTime = 0;
    [SerializeField]
    TextMeshProUGUI timer;

    int score = 0;
    public static int SCORE
    {
        get { return instance.score; }
        set
        {
            instance.score = value;
            ScoreUI.UpdateScore();
        }
    }
    public bool respawnNeeded = false;
    public GameObject player;
    public Transform respawnPoint;

    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentTime = timeToComplete;
    }

    // Update is called once per frame
    void Update()
    {
        Respawning();
    }

    public void UpdateTime()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0) currentTime = 0;
        timer.text = currentTime.ToString();
        if (currentTime <= 0) Debug.Log("Trop lent");
    }

    void Respawning()
    {
        if (respawnNeeded)
        {
            Instantiate(player, respawnPoint);
            respawnNeeded = false;
        }
    }

}
