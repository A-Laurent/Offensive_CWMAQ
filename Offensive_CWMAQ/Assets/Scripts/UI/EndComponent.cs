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

    //All canvas to activate if games end

    public GameObject RawImage;
    public GameObject PlayerInfo;
    public GameObject DiedText;
    public GameObject WinText;
    public GameObject Titles;
    public GameObject Button;

    private bool ActiveButton;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster = GameObject.Find("GameMaster");

        //Disable all canvas 
        DiedText.SetActive(false);
        WinText.SetActive(false);

        RawImage.SetActive(false);
        Titles.SetActive(false);
        PlayerInfo.SetActive(false);
        Button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.GetComponent<GameMaster>().IsPlayerDead)
        {
            DiedText.SetActive(true);
            ActiveEnd();
            ActiveEndButton();

            if (ActiveButton)
                Button.SetActive(true);
        }

        if (GameMaster.GetComponent<GameMaster>().IsPlayerWin)
        {
            WinText.SetActive(true);
            ActiveEnd();
            ActiveEndButton();

            if (ActiveButton)
                Button.SetActive(true);
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
