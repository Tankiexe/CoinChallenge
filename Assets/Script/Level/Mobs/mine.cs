using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int damage;

    public GameObject minesParicules;
    
    void Start()
    {
        Destroy(gameObject, 20);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Instantiate(minesParicules);
        playerController.instance.TakingDamage(damage);
        Destroy(gameObject);
    }
}
