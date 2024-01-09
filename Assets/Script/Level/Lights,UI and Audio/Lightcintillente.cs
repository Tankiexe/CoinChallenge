using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Lightcintillante : MonoBehaviour
{
    Light light;
    [SerializeField]
    bool lum = false;

    [SerializeField]
    AnimationCurve lightCurve;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(FlashingCorout());
    }

    

    IEnumerator Scintillement()
    {
        float timeCount = 0;
        while (true)
        {
            if (timeCount < 0.2f)
            {
                timeCount = timeCount + Time.deltaTime;
            }
            else if (lum == true)
            {
                light.intensity = 2222;
                lum = false;
                timeCount = 0;
            }
            else if (lum == false)
            {
                light.intensity = 600;
                lum = true;
                timeCount = 0;
            }
            
            yield return null;
        }
    }

    IEnumerator FlashingCorout()
    {
        float lightIntensity = light.intensity;
        while (true)
        {
            light.intensity = lightIntensity * lightCurve.Evaluate(Time.time);
            
            yield return null;
        }
    }

}
