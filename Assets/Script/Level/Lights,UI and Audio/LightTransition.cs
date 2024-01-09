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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateLight()
    {
        float distFromA = Vector3.Distance(playerController.instance.transform.position, transforms[0].position);
        float distFromB = Vector3.Distance(playerController.instance.transform.position, transforms[1].position);
        StartCoroutine(SetSun(distFromA > distFromB));

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
            //sun.intensity = Mathf.Lerp(startRotation, targetRotation, t);
            t += Time.deltaTime / 10;

            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        updateLight();
    }

}
