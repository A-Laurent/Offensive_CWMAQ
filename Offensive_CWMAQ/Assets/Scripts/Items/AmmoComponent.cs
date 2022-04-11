using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoComponent : MonoBehaviour
{
    private int i;

    private float Timer;
    private float EnemyTimer;

    private bool IsInTrigger;
    private bool IsKeyPressed;

    private bool canBeDestroyed = false;

    private GameObject Player;

    private GameObject text;
    private GameObject fillBar;
    private GameObject fullAmmo;

    void Start()
    {
        text = GameObject.Find("Appuie sur E");
        fillBar = GameObject.Find("LoadingObject");
        fullAmmo = GameObject.Find("AmmoFull");

        //Search player to get AmmoManager  
        Player = GameObject.Find("Player");
        i = 0;

    }

    void Update()
    {

        //Destroy this.gameobject if Timer if over 1f
        if (canBeDestroyed)
            GameObject.Destroy(gameObject);


        if(i == 0)
        {
            //Set all canvas to false
            text.SetActive(false);
            fillBar.SetActive(false);
            fullAmmo.SetActive(false);
            i = 14;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Check if the trigger that we are in if an Ammo (AmmoManager) && if that's the player (Movement)

        if (other.gameObject.GetComponent<AmmoManager>() && other.gameObject.GetComponent<Movement>())
        {

            //if the player's ammo over 200, active the canva full ammo
            if (other.gameObject.GetComponent<AmmoManager>().Ammo >= 200)
            {
                fullAmmo.SetActive(true);
            }

            //if the player's ammo below 200, active canva (press E), and load the timer
            if (other.gameObject.GetComponent<AmmoManager>().Ammo < 200)
            {
                fullAmmo.SetActive(false);
                IsInTrigger = true;
                text.SetActive(true);
                CheckPressedTime();
            }

        }

        //If the collision that we are in is an ammo (AmmoManager) && we are the BOT

        if (other.gameObject.GetComponent<AmmoManager>() && other.gameObject.GetComponent<EnemyBT>())
        {

            //if bot ammo are full, do nothing
            if (other.gameObject.GetComponent<AmmoManager>().Ammo >= 200)
                return;

            //if bot ammo bellow 200, launch timer
            if (other.gameObject.GetComponent<AmmoManager>().Ammo < 200)
            {
                EnemyTimer += Time.deltaTime;


                //if timer > 2f, reload to 200 and destroy the ammo
                if (EnemyTimer > 2f)
                {
                    other.gameObject.GetComponent<AmmoManager>().Ammo = 200;
                    GameObject.Destroy(gameObject);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        //if collision exit, unload canvas, and set IsInTrigger to false
        IsInTrigger = false;

        fullAmmo.SetActive(false);
        fillBar.SetActive(false);
        text.SetActive(false);


        //if leaving zone while pressing E, reset timer and unload canvas 
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
                //if KeyPressed, active the canva and increase Time to Timer
                fillBar.SetActive(true);
                Timer += Time.deltaTime;

                //set KeyPressed to false to check if timer if over 1f
                IsKeyPressed = false;
            }
            else
            {
                if ((Time.time - Timer) > 1.0f)
                {
                    //if Timer bellow 1f, set keypressed to true to keep increase time to Timer
                    IsKeyPressed = true;
                }
            }

            //if Timer over 1f
            if (Timer >= 1f)
            {
                //Player's Ammo set to 200
                Player.GetComponent<AmmoManager>().Ammo = 200;

                //Canvas disabled
                text.SetActive(false);
                fillBar.SetActive(false);

                //Set canBeDestroyed to true to destroy the object in Update() 
                canBeDestroyed = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {

            //if E is up, all canvas disabled and Timer reset
            text.SetActive(false);
            fillBar.SetActive(false);
            Timer = 0;
            IsKeyPressed = true;
        }

        if(Input.GetKey(KeyCode.E) && !IsInTrigger)
        {
            //if E pressed and player leaving zone, unload fillBar
            fillBar.SetActive(false);
        }

        return canBeDestroyed;
    }
}
