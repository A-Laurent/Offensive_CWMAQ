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
    public GameObject SynopsisMenuBTN;
    public GameObject ReturnBTN;

    public GameObject Head;
    public GameObject Body;
    public GameObject Foot;
    public GameObject SynopsisBG;
    public GameObject ReturnMenuBTN;

    public EventSystem M_eventSystem;

    //Disable Map selector on Awake
    private void Start()
    {
        Map1.SetActive(false);
        Map2.SetActive(false);
        ReturnBTN.SetActive(false);

        Head.SetActive(false);
        Body.SetActive(false);
        Foot.SetActive(false);
        SynopsisBG.SetActive(false);
        ReturnMenuBTN.SetActive(false);
    }

    //Active mapselector and disable menuUI
    public void Play()
    {
        PlayButton.SetActive(false);
        SynopsisMenuBTN.SetActive(false);
        QuitButton.SetActive(false);
        Credits.SetActive(false);

        Map1.SetActive(true);
        Map2.SetActive(true);
        ReturnBTN.SetActive(true);

        M_eventSystem.SetSelectedGameObject(Map1);
        
    }

    public void Return()
    {
        PlayButton.SetActive(true);
        QuitButton.SetActive(true);
        SynopsisMenuBTN.SetActive(true);
        Credits.SetActive(true);

        Map1.SetActive(false);
        Map2.SetActive(false);
        ReturnBTN.SetActive(false);

        M_eventSystem.SetSelectedGameObject(PlayButton);
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

    public void Synopsis()
    {
        M_eventSystem.SetSelectedGameObject(ReturnMenuBTN);

        PlayButton.SetActive(false);
        SynopsisMenuBTN.SetActive(false);
        QuitButton.SetActive(false);
        Credits.SetActive(false);

        Head.SetActive(true);
        Body.SetActive(true);
        Foot.SetActive(true);
        SynopsisBG.SetActive(true);
        ReturnMenuBTN.SetActive(true);
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
