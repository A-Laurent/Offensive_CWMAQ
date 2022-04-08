using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoComponent : MonoBehaviour
{

    private float Timer;
    private float EnemyTimer;

    private bool IsInTrigger;
    private bool IsKeyPressed;

    private bool canBeDestroyed = false;

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
        if (canBeDestroyed)
            GameObject.Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<AmmoManager>() && other.gameObject.GetComponent<Movement>())
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
                CheckPressedTime();

                
            }

        }

        if(other.gameObject.GetComponent<AmmoManager>() && other.gameObject.GetComponent<EnemyBT>())
        {
            if (other.gameObject.GetComponent<AmmoManager>().Ammo >= 200)
                return;
            if (other.gameObject.GetComponent<AmmoManager>().Ammo < 200)
            {
                EnemyTimer += Time.deltaTime;

                if(EnemyTimer > 2f)
                {
                    other.gameObject.GetComponent<AmmoManager>().Ammo = 200;
                    GameObject.Destroy(gameObject);
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
    public bool CheckPressedTime()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (IsKeyPressed)
            {
                fillBar.SetActive(true);
                Timer += Time.deltaTime;
                IsKeyPressed = false;
            }
            else
            {
                if ((Time.time - Timer) > 1.0f)
                {
                    IsKeyPressed = true;
                }
            }
            if (Timer >= 1f)
            {
                Player.GetComponent<AmmoManager>().Ammo = 200;

                text.SetActive(false);
                fillBar.SetActive(false);

                canBeDestroyed = true;
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

        return canBeDestroyed;
    }
}
