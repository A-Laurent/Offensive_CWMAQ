using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject QuitButton;
    public GameObject Credits;
    public GameObject Map1;
    public GameObject Map2;
    public GameObject LoadingScreen;

    public Slider slider;
    public Text LoadingText;

    //Disable Map selector on Awake
    private void Start()
    {
        Map1.SetActive(false);
        Map2.SetActive(false);
        LoadingScreen.SetActive(false);
    }

    //Active mapselector and disable menuUI
    public void Play()
    {
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        Credits.SetActive(false);

        Map1.SetActive(true);
        Map2.SetActive(true);
    }

    //Load Map1
    public void Carte1(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }
    
    //Load Map2
    public void Carte2(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    //Quit the game
    public void Quit()
    {
        Application.Quit();
    }

    //Load MenuScene
    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    //LoadCredits
    public void GoCredits()
    {
        SceneManager.LoadScene("CréditsScene");
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            LoadingText.text = progress * 100 + "%";

            yield return null;
        }
    }
}
