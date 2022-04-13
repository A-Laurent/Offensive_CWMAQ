using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour  
{
    public GameObject Player;
    private bool CanResetTimeScale = false;

    private void Update()
    {
        if (CanResetTimeScale && Time.timeScale == 0)
            Time.timeScale = 1f;
    }
    public void ReturnMenu()
    {
        CanResetTimeScale = true;
        SceneManager.LoadScene("MenuScene");

    }
    public void GoCredits()
    {
        CanResetTimeScale = true;
        SceneManager.LoadScene("CréditsScene");
    }

    public void Resume()
    {
        Player.GetComponent<PauseComponent>().IsMenu = false;
    }
}
