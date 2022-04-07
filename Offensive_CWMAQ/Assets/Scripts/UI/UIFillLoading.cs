using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFillLoading : MonoBehaviour
{

    private GameObject Ammo;
    private float maxTime;

    public Image TimerFillBar;
    void Start()
    {
        maxTime = 2f;
        Ammo = GameObject.Find("Ammo");
    }

    // Loading fillbar with a timer
    void Update()
    {
        if (Ammo == null)
            return;

        TimerFillBar.fillAmount = Ammo.GetComponent<AmmoComponent>().Timer / maxTime;

        
    }
}
