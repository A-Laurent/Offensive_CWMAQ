using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIlife : MonoBehaviour
{

    private GameObject health;
    private float maxHealth;

    public Image healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100f;
        health = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health.GetComponent<HpManager>().Hp / maxHealth;
    }
}
