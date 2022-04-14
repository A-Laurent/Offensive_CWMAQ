using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndComponent : MonoBehaviour
{
    private GameObject GameMaster;

    //Need Canva to acces the button manager
    //public GameObject EndObj;

    //All canvas to desactivate if games end

    public GameObject PressE;
    public GameObject LoadingObject;
    public GameObject AmmoFull;

    public GameObject Ammos;
    public GameObject HealthBarBG;
    public GameObject HealthBar;
    public GameObject HealthBarBorder;
    public GameObject CursorIG;

    //All canvas to activate if games end

    public GameObject PlayerInfo;
    public GameObject RawImage;
    public GameObject DiedText;
    public GameObject WinText;
    public GameObject Titles;
    public GameObject ReturnToMenu;
    public GameObject Credits;
    public GameObject RankTextIG;
    public GameObject KillTextIG;

    public GameObject Scores;
    public GameObject Kills;
    public GameObject GameTime;

    //Event system to manage controller in menu
    public EventSystem M_EventSystem;

    private bool ActiveButton;
    private int i = 0;

    //Timer for make buttons appear
    private float Timer;
    private bool DoOnce = true;

    void Start()
    {
        //Get GameMaster
        GameMaster = GameObject.Find("GameMaster");

        //Disable all canvas 
        DiedText.SetActive(false);
        WinText.SetActive(false);

        RawImage.SetActive(false);
        Titles.SetActive(false);
        ReturnToMenu.SetActive(false);
        Credits.SetActive(false);
        PlayerInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Update playerInfos to print them on the EndScreen
        Scores.GetComponent<UnityEngine.UI.Text>().text = GameMaster.GetComponent<GameMaster>().PlayerAlive.Length.ToString();
        Kills.GetComponent<UnityEngine.UI.Text>().text = GameMaster.GetComponent<GameMaster>().Kills.ToString();
        GameTime.GetComponent<UnityEngine.UI.Text>().text = GameMaster.GetComponent<GameMaster>().UITimer.ToString() + "sec";

        if (GameMaster.GetComponent<GameMaster>().IsPlayerDead)
        {
            //if player dead, active endUI and print endText + playerinfos
            DiedText.SetActive(true);
            ActiveEnd();
            ActiveEndButton();


            //Active buttons
            if (ActiveButton)
            {     
                ReturnToMenu.SetActive(true);
                Credits.SetActive(true);

                //Frezze player only onetime
                if(DoOnce)
                    Time.timeScale = 0f;

                //if player hit GoCreditButton or ReturnMenuButton, unfreeze game
                if (GetComponent<ButtonManager>().CanResetTimeScale)
                    Time.timeScale = 1f;
            }
                
        }

        if (GameMaster.GetComponent<GameMaster>().IsPlayerWin)
        {
            //if player win, active WinUI and print endText + playerinfos
            WinText.SetActive(true);
            ActiveEnd();
            ActiveEndButton();

            //Active buttons
            if (ActiveButton)
            {
                Credits.SetActive(true);
                ReturnToMenu.SetActive(true);
                //Frezze player only onetime
                if (DoOnce)
                    Time.timeScale = 0f;

                //if player hit GoCreditButton or ReturnMenuButton, unfreeze game
                if (GetComponent<ButtonManager>().CanResetTimeScale)
                    Time.timeScale = 1f;
            }
                
        }
    }

    private void ActiveEnd()
    {

        //Disable all loading canvas
        PressE.SetActive(false);
        LoadingObject.SetActive(false);
        AmmoFull.SetActive(false);

        //Disable all UI canvas 
        Ammos.SetActive(false);
        HealthBar.SetActive(false);
        HealthBarBG.SetActive(false);
        HealthBarBorder.SetActive(false);
        CursorIG.SetActive(false);
        RankTextIG.SetActive(false);
        KillTextIG.SetActive(false);

        //Activate all canvas for EndGame
        RawImage.SetActive(true);
        Titles.SetActive(true);
        PlayerInfo.SetActive(true);

        if (i == 0)
        {
            //Active Cursor and set firstselectedobj to credits (controller)
            Cursor.visible = true;
            M_EventSystem.SetSelectedGameObject(Credits);

            //Do only this one time
            i = 14;
        }
    }

    //This is a timer to print buttons after 2f after games end
    private bool ActiveEndButton()
    {
        Timer += Time.deltaTime;

        if (Timer > 2f)
            ActiveButton = true;

        return ActiveButton;
    }
}
