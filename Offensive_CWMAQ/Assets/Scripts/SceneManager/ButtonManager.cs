using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //Those two fonctions allow the MainMenu to work correctly, QUIT to leave the game and Play to switch to the MainScene

    public void ReturnMenu(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    //public void GoCredits()
    //{
    //    SceneManager.LoadScene("CréditsScene");
    //}

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            yield return null;
        }
    }
}
