using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoComponent : MonoBehaviour
{

    public float Timer;
    private bool IsInTrigger;
    private bool IsKeyPressed;

    private GameObject Player;

    public GameObject text;
    public GameObject fillBar;
    public GameObject fullAmmo;

    void Start()
    {
        Player = GameObject.Find("Player");
        text.SetActive(false);
        fillBar.SetActive(false);
        fullAmmo.SetActive(false);
    }

    void Update()
    {
        CheckPressedTime();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<AmmoManager>())
        {


            if (other.gameObject.GetComponent<AmmoManager>().Ammo >= 200)
            {
                fullAmmo.SetActive(true);
            }


            if (other.gameObject.GetComponent<AmmoManager>().Ammo < 200)
            {
                fullAmmo.SetActive(false);
                IsInTrigger = true;
                text.SetActive(true);

                if (Timer >= 2f)
                {
                    Player.GetComponent<AmmoManager>().Ammo = 200;

                    text.SetActive(false);
                    fillBar.SetActive(false);

                    GameObject.Destroy(this.gameObject);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsInTrigger = false;

        fullAmmo.SetActive(false);
        fillBar.SetActive(false);
        text.SetActive(false);

        if(Input.GetKey(KeyCode.E))
        {
            Timer = 0;
            fillBar.SetActive(false);
            text.SetActive(false);
        }
    }


    //This function create a Timer that correspond to the pressed Time of E 
    public float CheckPressedTime()
    {
        if (Input.GetKey(KeyCode.E) && IsInTrigger)
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
            text.SetActive(false);
            fillBar.SetActive(false);
            Timer = 0;
            IsKeyPressed = true;
        }

        if(Input.GetKey(KeyCode.E) && !IsInTrigger)
        {
            fillBar.SetActive(false);
        }

        return Timer;
    }
}
