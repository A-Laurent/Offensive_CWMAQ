using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    public bool PlayDeadSound;
    private GameObject GameMaster;
    public int Hp;
    int i = 0;

    public float TimerUILife;

    private bool IsInTrigger;
    private bool IsKeyPressed;

    void Start()
    {
        Hp = 100;
        GameMaster = GameObject.Find("GameMaster");
    }

    void Update()
    {
        CheckPressedTime();

        //This condition allow the player to lose switch scene if the player has no HP left 

        if (Hp <= 0 && GetComponent<Movement>()&& i == 0)
        {
            //GameMaster.GetComponent<GameMaster>().CanMove = false;
            GameMaster.GetComponent<GameMaster>().IsPlayerDead = true;
            PlayDeadSound = true;
            i ++;
        }
        else
        {
            PlayDeadSound = false;
        }

        //This condition allow the bot to die if he has not hp left 
        
        if (Hp <= 0 && transform.GetComponent<HpManager>() && transform.GetComponent<EnemyBT>())
        {
            GameObject.Destroy(this.gameObject);                     
        }
    }

    private void OnTriggerStay(Collider other)
    {

        //if player enters in Ammo's trigger zone and Ammo's player < 200 then, IsInTriger = true
        if (other.gameObject.GetComponent<MedKitComp>())
        {
            if (Hp < 100)
            {
                IsInTrigger = true;
            }

        }
    }
    public float CheckPressedTime()
    {

        //Check if E is pressed in the trigger zone
        if (Input.GetKey(KeyCode.E) && IsInTrigger)
        {

            //if yes, timerUI increase the deltaTime
            if (IsKeyPressed)
            {
                TimerUILife += Time.deltaTime;
                IsKeyPressed = false;
            }
            else
            {
                if ((Time.time - TimerUILife) > 1.0f)
                {
                    IsKeyPressed = true;
                }
            }
        }
        //if E is UP, TimerUI reset
        if (Input.GetKeyUp(KeyCode.E))
        {
            TimerUILife = 0;
            IsKeyPressed = true;
        }

        return TimerUILife;
    }

}