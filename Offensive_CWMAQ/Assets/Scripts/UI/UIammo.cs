using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIammo : MonoBehaviour
{

    private GameObject Player; 
    public GameObject Score_text;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Print the ammo amount in screen
    void Update()
    {
        Score_text.GetComponent<UnityEngine.UI.Text>().text = Player.GetComponent<AmmoManager>().Ammo.ToString() + "/200";
    }
}
