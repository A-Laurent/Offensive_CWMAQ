using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PauseComponent : MonoBehaviour
{
    public GameObject PauseMenuImg;
    public GameObject PauseText;
    public GameObject ResumeBTN;
    public GameObject QUITBtn;
    public GameObject Player;
    public GameObject GameMaster;

    public EventSystem M_EventSystem;

    public bool IsMenu = false;

    // Start is called before the first frame update
    void Start()
    {
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
                        
            M_EventSystem.SetSelectedGameObject(ResumeBTN);
            IsMenu = true;
            Time.timeScale = 0f;

        }

        if (IsMenu && !GameMaster.GetComponent<GameMaster>().IsPlayerDead && !GameMaster.GetComponent<GameMaster>().IsPlayerWin)
        {
            Player.GetComponent<Movement>().CanMove = false;

            PauseMenuImg.SetActive(true);
            PauseText.SetActive(true);
            ResumeBTN.SetActive(true);
            QUITBtn.SetActive(true);


        }

        if (!IsMenu && !GameMaster.GetComponent<GameMaster>().IsPlayerDead && !GameMaster.GetComponent<GameMaster>().IsPlayerWin)
        {

            Time.timeScale = 1f;

            PauseMenuImg.SetActive(false);
            PauseText.SetActive(false);
            ResumeBTN.SetActive(false);
            QUITBtn.SetActive(false);

            Player.GetComponent<Movement>().CanMove = true;

        }
    }
}
