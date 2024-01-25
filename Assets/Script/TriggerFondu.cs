using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFondu : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("start");
        IHM.Instance.DoingFondu();
    }
}
