using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFillLoading : MonoBehaviour
{

    private GameObject Ammo;
    private float maxTime;

    public Image TimerFillBar;
    // Start is called before the first frame update
    void Start()
    {
        maxTime = 2f;
        Ammo = GameObject.Find("Ammo");
    }

    // Update is called once per frame
    void Update()
    {
        TimerFillBar.fillAmount = Ammo.GetComponent<AmmoManager>().Timer / maxTime;
    }
}
