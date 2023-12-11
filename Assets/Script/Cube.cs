using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("contact");
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("avec player");
        playerController.instance.playerLife -= 1;
        IHM.Instance.UpdateLifeBar();
    }

}
