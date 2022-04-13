using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    //Crating PlayerInfos

    public int Kills;
    public bool IsPlayerDead = false;
    public bool IsPlayerWin = false;

    public float Timer;
    public int UITimer;

    public Text RankText;
    public Text KillsText;

    public GameObject[] PlayerAlive;

    void Update()
    {
        //If player is dead, stop the timer, else increase it

        if (IsPlayerWin || IsPlayerDead)
            return;
        else
        {
            Timer += Time.deltaTime;
            UITimer = Mathf.RoundToInt(Timer);
        }
            
        //Get array of all obj that had enemy tag, if player is the last of the array, isPlayerWin = true

        PlayerAlive = GameObject.FindGameObjectsWithTag("Enemy");
        if(PlayerAlive.Length == 1 && IsPlayerDead==false)
        {
            IsPlayerWin = true;
        }

        KillsText.text = "Kills : " + Kills.ToString();
        RankText.text = PlayerAlive.Length.ToString() + "/100";
    }
}
