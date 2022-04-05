using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Text myText;

    //Those two fonctions allow the MainMenu to work correctly, QUIT to leave the game and Play to switch to the MainScene
    public void QuitButton()
    {
        myText.text = "QUIT";
        Application.Quit();
    }
  
    public void PlayButton()
    {
        myText.text = "Play";
        SceneManager.LoadScene("MainScene");        
    }
}
