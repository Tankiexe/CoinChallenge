using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    [SerializeField]
    int score;
    [SerializeField]
    float time;
    [SerializeField]
    bool win;

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
        //to do verifier si la partie est gagnée ou non
    }


}
