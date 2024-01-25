using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Lightcintillante : MonoBehaviour
{
    Light light;
    
    [SerializeField]
    AnimationCurve lightCurve;
    
    private void OnEnable()
    {
        light = GetComponent<Light>();
        StartCoroutine(FlashingCorout());
    }

    IEnumerator FlashingCorout()
    {
        float startDelay = Random.Range(0f, 1f);
        yield return new WaitForSeconds(startDelay);

        float lightIntensity = light.intensity;
        while (true)
        {
            light.intensity = lightIntensity * lightCurve.Evaluate(Time.time);
            
            yield return null;
        }
    }
}
