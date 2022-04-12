using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //Those two fonctions allow the MainMenu to work correctly, QUIT to leave the game and Play to switch to the MainScene

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GoCredits()
    {
        SceneManager.LoadScene("CréditsScene");
    }
}
