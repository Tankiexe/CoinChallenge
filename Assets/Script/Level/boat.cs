using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.gameObject.CompareTag("Player")) return;
        other.gameObject.transform.SetParent(transform, true);
        other.gameObject.GetComponent<playerController>().enabled = false;
        GameManager.instance.movingBoat = true;
        GameManager.instance.playerOnBoat = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        GameManager.instance.playerOnBoat = false;
    }
}
