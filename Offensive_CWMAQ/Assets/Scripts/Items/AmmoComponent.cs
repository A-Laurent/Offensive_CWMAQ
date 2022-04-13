using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoComponent : MonoBehaviour
{ 
    private float Timer;

    private bool IsInTrigger;
    private bool IsKeyPressed;
    private bool canBeDestroyed = false;

    private GameObject Player;
    public GameObject FillBar;

    public Text _Text;
    public Text FullAmmo;

    void Start()
    {

        //Set FillBarUI to false
        _Text.gameObject.SetActive(false);
        FillBar.gameObject.SetActive(false);
        FullAmmo.gameObject.SetActive(false);

    }

    void Update()
    {

        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

        if (canBeDestroyed)
            GameObject.Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        //Check if the trigger that we are in if an Ammo (AmmoManager) && if that's the player (Movement)

        if (other.gameObject.GetComponent<AmmoManager>() && other.gameObject.GetComponent<Movement>())
        {

            //if the player's ammo over 200, active the canva full ammo
            if (other.gameObject.GetComponent<AmmoManager>().Ammo >= 200)
            {
                FullAmmo.gameObject.SetActive(true);
            }

            //if the player's ammo below 200, active canva (press E), and load the timer
            if (other.gameObject.GetComponent<AmmoManager>().Ammo < 200)
            {
                FullAmmo.gameObject.SetActive(false);
                IsInTrigger = true;
                _Text.gameObject.SetActive(true);
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
                other.gameObject.GetComponent<AmmoManager>().Ammo = 100;
                GameObject.Destroy(this.gameObject);
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Movement>())
        {
            //if collision exit, unload canvas, and set IsInTrigger to false
            IsInTrigger = false;

            FullAmmo.gameObject.SetActive(false);
            FillBar.gameObject.SetActive(false);
            _Text.gameObject.SetActive(false);


            //if leaving zone while pressing E, reset timer and unload canvas 
            if (Input.GetButton("ButtonX"))
            {
                Timer = 0;
                FullAmmo.gameObject.SetActive(false);
                FillBar.gameObject.SetActive(false);
                _Text.gameObject.SetActive(false);
            }
        }

    }


    //This function create a Timer that correspond to the pressed Time of E 
    public bool CheckPressedTime()
    {
        if (Input.GetKey(KeyCode.E)||Input.GetButton("ButtonX"))
        {
            
            if (IsKeyPressed)
            {
                //if KeyPressed, active the canva and increase Time to Timer
                FillBar.gameObject.SetActive(true);
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
                _Text.gameObject.SetActive(false);
                FillBar.gameObject.SetActive(false);

                //Set canBeDestroyed to true to destroy the object in Update() 
                canBeDestroyed = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("ButtonX"))
        {

            //if E is up, all canvas disabled and Timer reset
            _Text.gameObject.SetActive(false);
            FillBar.gameObject.SetActive(false);
            Timer = 0;
            IsKeyPressed = true;
        }

        if(Input.GetKey(KeyCode.E) && !IsInTrigger || Input.GetButton("ButtonX") && !IsInTrigger)
        {
            //if E pressed and player leaving zone, unload fillBar
            FillBar.gameObject.SetActive(false);
        }

        return canBeDestroyed;
    }
}
