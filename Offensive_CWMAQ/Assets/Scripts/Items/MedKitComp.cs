using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedKitComp : MonoBehaviour
{
    private int i;

    private float Timer;
    private float EnemyTimer;

    private bool IsInTrigger;
    private bool IsKeyPressed;

    private bool canBeDestroyed = false;

    private GameObject Player;

    public Text _Text;
    public GameObject FillBar;
    public Text FullLife;

    private bool isInit = false;

    void Start()
    {
        _Text.gameObject.SetActive(false);
        FillBar.gameObject.SetActive(false);
        FullLife.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }



        //Search player to get HpManager  

        //Destroy this.gameobject if Timer if over 1f
        if (canBeDestroyed)
            GameObject.Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.GetComponent<HpManager>() && other.gameObject.GetComponent<Movement>())
        {
            //if the player's ammo over 200, active the canva full ammo
            if (other.gameObject.GetComponent<HpManager>().Hp >= 100)
            {
                FullLife.gameObject.SetActive(true);
            }

            //if the player's ammo below 200, active canva (press E), and load the timer
            if (other.gameObject.GetComponent<HpManager>().Hp < 100)
            {
                FullLife.gameObject.SetActive(false);
                IsInTrigger = true;
                _Text.gameObject.SetActive(true);
                CheckPressedTime();
            }
        }


        //If the collision that we are in is an ammo (HpManager) && we are the BOT

        if (other.gameObject.GetComponent<EnemyBT>())
        {
            //if bot life is full, do nothing
            if (other.gameObject.GetComponent<HpManager>().Hp >= 100)
                return;

            other.gameObject.GetComponent<HpManager>().Hp = 100;
            GameObject.Destroy(this.gameObject);

        }


    }


    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Movement>())
        {
    //if collision exit, unload canvas, and set IsInTrigger to false
        IsInTrigger = false;

        FullLife.gameObject.SetActive(false);
        FillBar.gameObject.SetActive(false);
        _Text.gameObject.SetActive(false);


        //if leaving zone while pressing E, reset timer and unload canvas 
        if (Input.GetButton("ButtonX"))
        {
            Timer = 0;
            FullLife.gameObject.SetActive(false);
            FillBar.gameObject.SetActive(false);
            _Text.gameObject.SetActive(false);
        }
        }
    
    }


    //This function create a Timer that correspond to the pressed Time of E 
    public bool CheckPressedTime()
    {
        if (Input.GetButton("ButtonX"))
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
                Player.GetComponent<HpManager>().Hp = 100;

                //Canvas disabled
                _Text.gameObject.SetActive(false);
                FillBar.gameObject.SetActive(false);

                //Set canBeDestroyed to true to destroy the object in Update() 
                canBeDestroyed = true;
            }
        }

        if (Input.GetButtonUp("ButtonX"))
        {

            //if E is up, all canvas disabled and Timer reset
            _Text.gameObject.SetActive(false);
            FillBar.gameObject.SetActive(false);
            Timer = 0;
            IsKeyPressed = true;
        }

        if (Input.GetButton("ButtonX") && !IsInTrigger)
        {
            //if E pressed and player leaving zone, unload fillBar
            FillBar.gameObject.SetActive(false);
        }

        return canBeDestroyed;
    }
}
