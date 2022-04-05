using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Text myText;

    public void QuitButton()
    {
        myText.text = "QUIT";
        Application.Quit();
    }
  
    public void PlayButton()
    {
        //SceneManager.LoadScene(gamescene)
        myText.text = "Play";
    }
}
