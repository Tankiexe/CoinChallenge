using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int timeToComplete = 113;
    public float currentTime = 0;
    public float elapsedTime
    {
        get { return timeToComplete - currentTime; }
    }
    

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
    public int killCount, coinsCollected;


    public bool respawnNeeded = false;
    public GameObject player;
    public Transform respawnPoint;

    public bool movingBoat = false;
    public bool playerOnBoat = false;
    public GameObject boat;
    
    


    public static GameManager instance;

    void Start()
    {
        instance = this;
        currentTime = timeToComplete;
        StartCoroutine(MovingBoatCorout());
    }

    void Update()
    {
        UpdateTime();
        Respawning();
    }

    /// <summary>
    /// Met a jour le timer et déclanche la fin de partie s'il ateint zero.
    /// </summary>
    public void UpdateTime()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0) currentTime = 0;

        IHM.Instance.UppdateTimer();
        if (currentTime <= 0) movingBoat = true;
    }

    IEnumerator MovingBoatCorout()
    {
        float boatZ = boat.transform.position.z;
        float boatStartingZ = boat.transform.position.z;
        float movement = 0;

        while (true)
        {
            while (movingBoat)
            {
                movement += Time.deltaTime * 5;
                boatZ += movement;
                boat.transform.position = new Vector3(boat.transform.position.x, boat.transform.position.y, boatZ);
                boatZ = boatStartingZ;

                if (movement > 50)
                {
                    PersistentData.instance.GetData();
                    SceneManager.LoadScene("End");
                }
                yield return null;
            }
            yield return null;
        }
        
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
