using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    
    public int score;
    public float time;
    public bool playerBoarded;

    public static PersistentData instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void GetData()
    {
        score = GameManager.SCORE;
        time = GameManager.instance.elapsedTime;
        playerBoarded = GameManager.instance.playerOnBoat;
        //to do verifier si la partie est gagnée ou non
    }


}
