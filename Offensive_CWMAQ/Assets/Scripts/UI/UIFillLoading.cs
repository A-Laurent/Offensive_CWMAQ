using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFillLoading : MonoBehaviour
{

    public GameObject Player;

    private float maxTime;

    public Image AmmoFillBar;
    public Image LifeFillBar;
    void Start()
    {
        maxTime = 1f;
    }

    // Loading fillbar with a timer
    void Update()
    {
        AmmoFillBar.fillAmount = Player.GetComponent<AmmoManager>().TimerUI / maxTime;
        LifeFillBar.fillAmount = Player.GetComponent<HpManager>().TimerUILife / maxTime;
    }
}
