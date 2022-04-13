using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIlife : MonoBehaviour
{
    private GameObject health;

    public Image healthBar;

    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        //Set maximum of player's life
        maxHealth = 100f;
        health = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //fill lifebar with player's hp / maxPlayerHealth and print it on UI
        healthBar.fillAmount = health.GetComponent<HpManager>().Hp / maxHealth;
    }
}
