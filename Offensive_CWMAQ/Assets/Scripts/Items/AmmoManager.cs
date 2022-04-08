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
        Ammo = 200;
    }
    void Update()
    {
        if (Ammo <= 0)
            Ammo = 0;

        CheckPressedTime();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<AmmoComponent>())
        {
            if (Ammo < 200)
            {
                IsInTrigger = true;
            }

        }
    }

    public float CheckPressedTime()
    {
        if (Input.GetKey(KeyCode.E) && IsInTrigger)
        {
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

        if (Input.GetKeyUp(KeyCode.E))
        {
            TimerUI = 0;
            IsKeyPressed = true;
        }

        return TimerUI;
    }


}
