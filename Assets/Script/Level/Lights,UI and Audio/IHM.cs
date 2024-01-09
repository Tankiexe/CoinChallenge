using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IHM : MonoBehaviour
{
    public static IHM Instance;

    [SerializeField]
    TextMeshProUGUI timer;
    [SerializeField]
    Image healthbar;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateLifeBar()
    {
        
        healthbar.fillAmount = (float)playerController.instance.playerLife / (float)playerController.instance.playerMaxLife;
    }

    public void UppdateTimer()
    {
        timer.text = ((int)GameManager.instance.currentTime).ToString();
    }

}
