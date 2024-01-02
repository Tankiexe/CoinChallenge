using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    [SerializeField]
    Transform respawnPoint;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerEnter(other);
        
    }

    void OnPlayerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        PersistentData.instance.GetData();
        SceneManager.LoadSceneAsync("End");
        //other.transform.position = respawnPoint.position;
        //GameManager.instance.respawnNeeded = true;
        //playerController.instance.playerLife -= 10;
        //IHM.Instance.UpdateLifeBar();
    }

}
