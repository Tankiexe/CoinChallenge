using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int timeToComplete = 120;
    float currentTime = 0;
    public float elapsedTime
    {
        get { return timeToComplete - currentTime; }
    }
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

        timer.text = ((int)currentTime).ToString();
        if (currentTime <= 0) Debug.Log("Trop lent");
    }

    void Respawning()
    {
        if (respawnNeeded)
        {
            playerController.instance.playerLife = 10;
            IHM.Instance.UpdateLifeBar();
            player.transform.position = respawnPoint.position; 
            respawnNeeded = false;
            playerController.instance.isDead = false;
        }
    }

}
