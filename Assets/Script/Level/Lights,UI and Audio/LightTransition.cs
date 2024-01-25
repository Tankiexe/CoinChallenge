using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTransition : MonoBehaviour
{
    [SerializeField]
    List<Transform> transforms;
    [SerializeField]
    Light sun;
    [SerializeField]
    float dayLightRotation , nightLightRotation ;
    [SerializeField]
    Color nightColor , sunColor;
    [SerializeField]
    int nightTemp , sunTemp ;

    [SerializeField]
    List<GameObject> lights;
    
    /// <summary>
    /// Met a jour toute les lights non baked.
    /// </summary>
    void updateLight()
    {
        float distFromA = Vector3.Distance(playerController.instance.transform.position, transforms[0].position);
        float distFromB = Vector3.Distance(playerController.instance.transform.position, transforms[1].position);
        StartCoroutine(SetSun(distFromA > distFromB));
        StartLights(distFromA > distFromB);
    }

    void StartLights(bool day)
    {
        if (day)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
            }
        }
        else
        {
            foreach(GameObject light in lights)
            {
                light.SetActive(false);
            }
        }
    }

    IEnumerator SetSun(bool day)
    {
        float startRotation = (day == true) ? nightLightRotation : dayLightRotation;
        float targetRotation = (day == true) ? dayLightRotation : nightLightRotation;

        float startTemp = (day == true) ? nightTemp : sunTemp;
        float targetTemp = (day == true) ? sunTemp : nightTemp;

        Color startColor = (day == true) ? nightColor : sunColor;
        Color targetColor = (day == true) ? sunColor : nightColor;

        float t = 0;
        while (t < 1.1)
        {
            sun.color = Color.Lerp(startColor, targetColor, t);
            sun.colorTemperature = Mathf.Lerp(startTemp, targetTemp, t);
            sun.transform.rotation = Quaternion.Euler(Mathf.Lerp(startRotation, targetRotation, t), 0, 0);
            
            t += Time.deltaTime / 6;

            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        updateLight();
    }
}
