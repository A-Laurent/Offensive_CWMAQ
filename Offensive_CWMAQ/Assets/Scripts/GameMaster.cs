using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public float Timer;
    public int Kills;
    public bool IsPlayerDead = false;
    public bool IsPlayerWin = false;

    void Update()
    {
        GameObject[] playerAlive = GameObject.FindGameObjectsWithTag("Enemy");
        if(playerAlive.Length == 1 && IsPlayerDead==false)
        {
            IsPlayerWin = true;
        }

        if (SceneManager.GetActiveScene().name == ("MainScene"))
        {
            Timer += Time.deltaTime;
        }
    }
}
