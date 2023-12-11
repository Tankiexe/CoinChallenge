using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IHM : MonoBehaviour
{
    public static IHM Instance;
    [SerializeField]
    Slider lifeBar;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.UpdateTime();
        
    }

    public void UpdateLifeBar()
    {
        lifeBar.SetValueWithoutNotify(playerController.instance.playerLife);
    }

}
