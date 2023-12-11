using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]
    int coinValue = 1;
    void Start()
    {

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        GameManager.SCORE += coinValue;
        Destroy(gameObject);
    }
}