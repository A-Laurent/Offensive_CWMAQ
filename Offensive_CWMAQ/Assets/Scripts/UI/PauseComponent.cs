using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PauseComponent : MonoBehaviour
{
    //Take all of PauseMenuObjects
    public GameObject PauseMenuImg;
    public GameObject PauseText;
    public GameObject ResumeBTN;
    public GameObject QUITBtn;
    public GameObject Player;
    public GameObject GameMaster;

    //Take EventSystem to manage the controller
    public EventSystem M_EventSystem;

    //Create bool to manage when pause is active
    public bool IsMenu = false;
    void Start()
    {
        //Set all pauseMenuUI to false;
        PauseMenuImg.SetActive(false);
        PauseText.SetActive(false);
        ResumeBTN.SetActive(false);
        QUITBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel") && !GameMaster.GetComponent<GameMaster>().IsPlayerDead && !GameMaster.GetComponent<GameMaster>().IsPlayerWin)
        {
            //if we hit Cancel (escape / B) and the game is not ended
            //Set Selected gameobject to Resume(controller)
            M_EventSystem.SetSelectedGameObject(ResumeBTN);
           
            IsMenu = true;
            
            //freeze game
            Time.timeScale = 0f;

        }

        if (IsMenu && !GameMaster.GetComponent<GameMaster>().IsPlayerDead && !GameMaster.GetComponent<GameMaster>().IsPlayerWin)
        {

            //if we are in menu, player can't move
            Player.GetComponent<Movement>().CanMove = false;

            //set active all PauseMenuUI
            PauseMenuImg.SetActive(true);
            PauseText.SetActive(true);
            ResumeBTN.SetActive(true);
            QUITBtn.SetActive(true);
        }

        if (!IsMenu && !GameMaster.GetComponent<GameMaster>().IsPlayerDead && !GameMaster.GetComponent<GameMaster>().IsPlayerWin)
        {
            //If we are not in PauseMenu and game is not ended
            //Unfreeze game
            Time.timeScale = 1f;

            //Disable PauseMenuUI
            PauseMenuImg.SetActive(false);
            PauseText.SetActive(false);
            ResumeBTN.SetActive(false);
            QUITBtn.SetActive(false);

            //PlayerCanMove
            Player.GetComponent<Movement>().CanMove = true;
        }
    }
}
