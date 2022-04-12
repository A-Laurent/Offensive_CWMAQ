using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject QuitButton;
    public GameObject Credits;
    public GameObject Map1;
    public GameObject Map2;

    public EventSystem M_eventSystem;

    //Disable Map selector on Awake
    private void Start()
    {
        Map1.SetActive(false);
        Map2.SetActive(false);
    }

    //Active mapselector and disable menuUI
    public void Play()
    {
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        Credits.SetActive(false);

        Map1.SetActive(true);
        Map2.SetActive(true);

        M_eventSystem.SetSelectedGameObject(Map1);
        
    }

    //Load Map1
    public void Carte1()
    {
        SceneManager.LoadScene("Carte1");
    }
    
    //Load Map2
    public void Carte2()
    {
        SceneManager.LoadScene("Carte2");
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
}
