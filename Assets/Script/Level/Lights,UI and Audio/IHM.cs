using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IHM : MonoBehaviour
{
    public static IHM Instance;

    [SerializeField]
    TextMeshProUGUI timer;
    [SerializeField]
    Image healthbar;
    
    public Image damage;
    public float damageAlpha = 0;

    [SerializeField]
    Image fondFondu;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateDamageScreen();
    }

    public void UpdateLifeBar()
    {
        
        healthbar.fillAmount = (float)playerController.instance.playerLife / (float)playerController.instance.playerMaxLife;
    }

    public void UppdateTimer()
    {
        timer.text = ((int)GameManager.instance.currentTime).ToString();
    }

    public void DoingFondu()
    {
        
        StartCoroutine(ScreenFadeCorout());
        
    }

    IEnumerator ScreenFadeCorout() 
    {
        float t = 0;
        
        Color color = fondFondu.color;
        while (t < 1.1f)
        {
            color.a = Mathf.Lerp(0, 1, t);
            fondFondu.color = color;
            t += Time.deltaTime;
            
            yield return null;
        }
    }

    void UpdateDamageScreen()
    {
        damage.color = new Color(damage.color.r, damage.color.g, damage.color.b, damageAlpha);
        damageAlpha -= Time.deltaTime / 2;
        if (damageAlpha < 0) damageAlpha = 0;
    }
}
