using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIammo : MonoBehaviour
{
    private GameObject Player; 

    public GameObject Score_text;

    void Start()
    {
        //Search player
        Player = GameObject.Find("Player");
    }

    // Print the ammo amount in screen
    void Update()
    {
        //Print Nb of ammo on the UI
        Score_text.GetComponent<UnityEngine.UI.Text>().text = Player.GetComponent<AmmoManager>().Ammo.ToString() + "/200";
    }
}
