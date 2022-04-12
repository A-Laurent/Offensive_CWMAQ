using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitComp : MonoBehaviour
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
    private GameObject fullLife;

    void Start()
    {
        text = GameObject.Find("Appuie sur E");
        fillBar = GameObject.Find("LoadingLifeObject");
        fullLife = GameObject.Find("LifeFull");

        //Search player to get HpManager  
        Player = GameObject.Find("Player");
        i = 0;

    }

    private void Update()
    {

        //Destroy this.gameobject if Timer if over 1f
        if (canBeDestroyed)
            GameObject.Destroy(gameObject);


        if (i == 0)
        {
            //Set all canvas to false
            text.SetActive(false);
            fillBar.SetActive(false);
            fullLife.SetActive(false);
            i = 14;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<HpManager>() && other.gameObject.GetComponent<Movement>())
        {

            //if the player's ammo over 200, active the canva full ammo
            if (other.gameObject.GetComponent<HpManager>().Hp >= 100)
            {
                fullLife.SetActive(true);
            }

            //if the player's ammo below 200, active canva (press E), and load the timer
            if (other.gameObject.GetComponent<HpManager>().Hp < 100)
            {
                fullLife.SetActive(false);
                IsInTrigger = true;
                text.SetActive(true);
                CheckPressedTime();
            }
        }

        //If the collision that we are in is an ammo (HpManager) && we are the BOT

        if (other.gameObject.GetComponent<HpManager>() && other.gameObject.GetComponent<EnemyBT>())
        {

            //if bot life is full, do nothing
            if (other.gameObject.GetComponent<HpManager>().Hp >= 100)
                return;

            //if bot life is bellow 100, launch timer
            if (other.gameObject.GetComponent<HpManager>().Hp < 100)
            {
                EnemyTimer += Time.deltaTime;

                //if timer > 2f, refill to 100 and destroy the life_obj
                if (EnemyTimer > 2f)
                {
                    other.gameObject.GetComponent<HpManager>().Hp = 100;
                    GameObject.Destroy(this.gameObject);
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {

        //if collision exit, unload canvas, and set IsInTrigger to false
        IsInTrigger = false;

        fullLife.SetActive(false);
        fillBar.SetActive(false);
        text.SetActive(false);


        //if leaving zone while pressing E, reset timer and unload canvas 
        if (Input.GetKey(KeyCode.E))
        {
            Timer = 0;
            fullLife.SetActive(false);
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
                Player.GetComponent<HpManager>().Hp = 100;

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

        if (Input.GetKey(KeyCode.E) && !IsInTrigger)
        {
            //if E pressed and player leaving zone, unload fillBar
            fillBar.SetActive(false);
        }

        return canBeDestroyed;
    }
}
