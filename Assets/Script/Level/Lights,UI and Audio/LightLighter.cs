using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLighter : MonoBehaviour
{
    public List<GameObject> lights;
    public GameObject sun;

    private void Start()
    {
        foreach (var light in lights) light.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        foreach (var light in lights)  light.SetActive(true); 
        //sun.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        //sun.SetActive(true);
        foreach (var light in lights) light.SetActive(false);
    }
}
