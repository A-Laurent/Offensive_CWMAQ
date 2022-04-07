using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{

    public float Timer;
    private bool IsKeyPressed;

    private GameObject Player;

    public GameObject text;
    public GameObject fillBar;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        CheckPressedTime();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<AmmoComponent>())
        {
                IsKeyPressed = true;
                text.SetActive(true);
                if (other.gameObject.GetComponent<AmmoComponent>().Ammo<=200 && Timer >= 2f)
                {
                    Player.GetComponent<AmmoComponent>().Ammo = 200;
                    GameObject.Destroy(this.gameObject);
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        fillBar.SetActive(false);
        text.SetActive(false);
    }


    //This function create a Timer that correspond to the pressed Time of E 
    public float CheckPressedTime()
    {
        if (Input.GetKey(KeyCode.E) && IsKeyPressed)
        {
            if (IsKeyPressed)
            {
                fillBar.SetActive(true);
                Timer += 0.01f;
                IsKeyPressed = false;
            }
            else
            {
                if ((Time.time - Timer) > 2.0f)
                {
                    IsKeyPressed = true;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            fillBar.SetActive(false);
            Timer = 0;
            IsKeyPressed = true;
        }

        return Timer;
    }
}
