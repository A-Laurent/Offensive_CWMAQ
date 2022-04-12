using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{

    //This component create & set the ammo value to 200

    public float TimerUI;
    public int Ammo;

    private bool IsInTrigger;
    private bool IsKeyPressed;

    void Start()
    {
        //Set Ammo to 200
        Ammo = 200;
    }
    void Update()
    {
        //Ammo can be bellow 0
        if (Ammo <= 0)
            Ammo = 0;


        //Timer plays all frames
        CheckPressedTime();
    }

    private void OnTriggerStay(Collider other)
    {

        //if player enters in Ammo's trigger zone and Ammo's player < 200 then, IsInTriger = true
        if (other.gameObject.GetComponent<AmmoComponent>())
        {
            if (Ammo < 200)
            {
                IsInTrigger = true;
            }

        }
    }


    //This function create a timer to fill the loading bar in the canva (fillLoading script)
    public float CheckPressedTime()
    {

        //Check if E is pressed in the trigger zone
        if (Input.GetKey(KeyCode.E) && IsInTrigger|| Input.GetButton("ButtonX") && IsInTrigger)
        {

            //if yes, timerUI increase the deltaTime
            if (IsKeyPressed)
            {
                TimerUI += Time.deltaTime;
                IsKeyPressed = false;
            }
            else
            {
                if ((Time.time - TimerUI) > 1.0f)
                {
                    IsKeyPressed = true;
                }
            }
        }
        //if E is UP, TimerUI reset
        if (Input.GetKeyUp(KeyCode.E))
        {
            TimerUI = 0;
            IsKeyPressed = true;
        }

        return TimerUI;
    }


}
