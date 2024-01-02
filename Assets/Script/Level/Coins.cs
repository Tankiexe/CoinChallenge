using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]
    int coinValue = 1;
    


    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        AudioLevelManeger.Instance.ToPlaySound(AudioLevelManeger.Instance.gling);
        GameManager.SCORE += coinValue;
        Destroy(gameObject);
    }
}