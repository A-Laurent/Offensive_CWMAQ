using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFillLoading : MonoBehaviour
{
    //Search player and some UI Objects
    public GameObject Player;

    public Image AmmoFillBar;
    public Image LifeFillBar;

    //create float that is the max filling time of the bar
    private float maxTime;

    void Start()
    {
        //Set maxFillingTime to 1f
        maxTime = 1f;
    }

    
    void Update()
    {
        // Loading fillbar (ammo/life) with a timer
        AmmoFillBar.fillAmount = Player.GetComponent<AmmoManager>().TimerUI / maxTime;
        LifeFillBar.fillAmount = Player.GetComponent<HpManager>().TimerUILife / maxTime;
    }
}
