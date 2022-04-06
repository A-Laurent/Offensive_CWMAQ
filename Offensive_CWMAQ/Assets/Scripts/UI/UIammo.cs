using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIammo : MonoBehaviour
{

    private GameObject Nb_ammo; 
    public GameObject Score_text;

    void Start()
    {
        Nb_ammo = GameObject.Find("Player");
    }

    // Print the ammo amount in screen
    void Update()
    {
        Score_text.GetComponent<UnityEngine.UI.Text>().text = Nb_ammo.GetComponent<AmmoComponent>().Ammo.ToString() + "/200";
    }
}
