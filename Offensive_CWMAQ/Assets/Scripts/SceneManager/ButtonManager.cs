using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject Player;
    public bool CanResetTimeScale = false;

    private void Update()
    {
    }
    public void ReturnMenu()
    {
        CanResetTimeScale = true;

        if(Time.timeScale == 1f)
            SceneManager.LoadScene("MenuScene");

    }
    public void GoCredits()
    {
        CanResetTimeScale = true;

        if (Time.timeScale == 1f)
            SceneManager.LoadScene("CréditsScene");
    }

    public void Resume()
    {
        Player.GetComponent<PauseComponent>().IsMenu = false;
    }
}