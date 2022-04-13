using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    //Get all UI Menu GameObjects to manage them during the player uses the menu


    //Buttons before clicking on play
    public GameObject PlayButton;    
    public GameObject Credits;
    public GameObject SynopsisMenuBTN;
    public GameObject QuitButton;

    //Buttons after clicked on play
    public GameObject Map1;
    public GameObject Map2;
    public GameObject ReturnBTN;

    //UI after clicked on Synopsis Button
    public GameObject Head;
    public GameObject Body;
    public GameObject Foot;
    public GameObject SynopsisBG;
    public GameObject ReturnMenuBTN;

    //Event system to allow the player to play on menu with controller
    public EventSystem M_eventSystem;
    private void Start()
    {
        //Disable Map selector on Awake
        Map1.SetActive(false);
        Map2.SetActive(false);
        ReturnBTN.SetActive(false);
        
        //Disable synopsis UI
        Head.SetActive(false);
        Body.SetActive(false);
        Foot.SetActive(false);
        SynopsisBG.SetActive(false);
        ReturnMenuBTN.SetActive(false);
    }
    public void Play()
    {
        //Disable MenuUI
        PlayButton.SetActive(false);
        SynopsisMenuBTN.SetActive(false);
        QuitButton.SetActive(false);
        Credits.SetActive(false);

        //Set mapselector active
        Map1.SetActive(true);
        Map2.SetActive(true);
        ReturnBTN.SetActive(true);

        //Set first element selected in eventsystem to map1 (controller)
        M_eventSystem.SetSelectedGameObject(Map1);
        
    }

    public void Return()
    {
        //Re-active MenuUI
        PlayButton.SetActive(true);
        QuitButton.SetActive(true);
        SynopsisMenuBTN.SetActive(true);
        Credits.SetActive(true);

        //Disable MapSelector
        Map1.SetActive(false);
        Map2.SetActive(false);
        ReturnBTN.SetActive(false);

        //Set first element selected in eventsystem to PlayButton (controller)
        M_eventSystem.SetSelectedGameObject(PlayButton);
    }

    
    public void Carte1()
    {
        //Load Map1
        SceneManager.LoadScene("Carte1", LoadSceneMode.Single);
    }
    
    
    public void Carte2()
    {
        //Load Map2
        SceneManager.LoadScene("Carte2");
    }

    public void Synopsis()
    {
        //Set first element selected in eventsystem to ReturnMenuBTN (controller) 
        M_eventSystem.SetSelectedGameObject(ReturnMenuBTN);

        //Disable MenuUI
        PlayButton.SetActive(false);
        SynopsisMenuBTN.SetActive(false);
        QuitButton.SetActive(false);
        Credits.SetActive(false);


        //Set active all Synopsis Menu
        Head.SetActive(true);
        Body.SetActive(true);
        Foot.SetActive(true);
        SynopsisBG.SetActive(true);
        ReturnMenuBTN.SetActive(true);
    }

    
    public void Quit()
    {
        //Quit the game
        Application.Quit();
    }

    
    public void ReturnMenu()
    {
        //Load MenuScene
        SceneManager.LoadScene("MenuScene");
        
    }

   
    public void GoCredits()
    {
        //LoadCredits
        SceneManager.LoadScene("CréditsScene");
    }
}
