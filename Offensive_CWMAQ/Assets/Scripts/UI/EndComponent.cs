using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndComponent : MonoBehaviour
{
    private GameObject GameMaster;


    //All canvas to desactivate if games end

    public GameObject PressE;
    public GameObject LoadingObject;
    public GameObject AmmoFull;

    public GameObject Ammos;
    public GameObject HealthBarBG;
    public GameObject HealthBar;
    public GameObject HealthBarBorder;
    public GameObject Cursor;

    //All canvas to activate if games end

    public GameObject PlayerInfo;
    public GameObject RawImage;
    public GameObject DiedText;
    public GameObject WinText;
    public GameObject Titles;
    public GameObject ReturnToMenu;
    public GameObject Credits;

    public GameObject Scores;
    public GameObject Kills;
    public GameObject GameTime;

    private bool ActiveButton;

    //Timer for make buttons appear
    private float Timer;

    // Start is called before the first frame update
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
        Scores.GetComponent<UnityEngine.UI.Text>().text = GameMaster.GetComponent<GameMaster>().playerAlive.Length.ToString();
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
                Time.timeScale = 0f;
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
                Time.timeScale = 0f;
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
        Cursor.SetActive(false);

        //Activate all canvas for EndGame
        RawImage.SetActive(true);
        Titles.SetActive(true);
        PlayerInfo.SetActive(true);
    }

    private bool ActiveEndButton()
    {
        Timer += Time.deltaTime;

        if (Timer > 2f)
            ActiveButton = true;

        return ActiveButton;
    }
}
