using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFillLoading : MonoBehaviour
{

    public GameObject AmmoManagerObject;
    private float maxTime;

    public Image TimerFillBar;
    void Start()
    {
        maxTime = 1f;
    }

    // Loading fillbar with a timer
    void Update()
    {
        if (AmmoManagerObject == null)
            return;

        TimerFillBar.fillAmount = AmmoManagerObject.GetComponent<AmmoManager>().TimerUI / maxTime;        
    }
}
