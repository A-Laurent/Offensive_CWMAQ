using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    //Player
    public float NormalSpeed = 5.0f;
    public float RunSpeed = 9.0f;
    public float JumpSpeed = 6.0f;
    public float Gravity = 20.0f;
    private float nextPlay;
    private float delay;
    public float delayBetweenStep;

    public bool IsMenu = false;

    public GameObject PauseMenuImg;
    public GameObject PauseText;
    public GameObject ResumeBTN;
    public GameObject QUITBtn;

    public EventSystem M_EventSystem;

    CharacterController Cc;

    Vector3 Deplacements;

    public Animator Anim;

    //Camera 
    public Camera HeadPlayer;
    
    void Start()
    {
        PauseMenuImg.SetActive(false);
        PauseText.SetActive(false);
        ResumeBTN.SetActive(false);
        QUITBtn.SetActive(false);

        //suppr cursor
        Cursor.visible = false;
        //Get Charactere controler component
        Cc = GetComponent<CharacterController>();
        delay = delayBetweenStep;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            IsMenu = true;
            Time.timeScale = 0f;
            M_EventSystem.SetSelectedGameObject(ResumeBTN);
        }

        if(IsMenu)
        {
            PauseMenuImg.SetActive(true);
            PauseText.SetActive(true);
            ResumeBTN.SetActive(true);
            QUITBtn.SetActive(true);
        }

        if(!IsMenu)
        {
            Time.timeScale = 1f;

            PauseMenuImg.SetActive(false);
            PauseText.SetActive(false);
            ResumeBTN.SetActive(false);
            QUITBtn.SetActive(false);

            Movements();
        }
           
    }

    private void Movements()
    {
        //Initializing movement vectors
        Vector3 z = transform.TransformDirection(Vector3.forward);
        Vector3 x = transform.TransformDirection(Vector3.right);

        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");

        //The jumping variables
        float speedY = Deplacements.y;

        //The sprint
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("LJoyStickClick")) 
        {
            delayBetweenStep = delay / 2;
            Anim.SetBool("WalkFr", false);
            Anim.SetBool("Run", true);
            speedX = speedX * RunSpeed;
            speedZ = speedZ * RunSpeed;
        }
        else
        {
            delayBetweenStep = delay;
            speedX = speedX * NormalSpeed;
            speedZ = speedZ * NormalSpeed;
            Anim.SetBool("Run", false);
            Anim.SetBool("WalkFr", true);
        }

        Deplacements = z * speedZ + x * speedX;

        //The jump
        if (Input.GetButton("Jump") && Cc.isGrounded||Input.GetButtonDown("ButtonA") &&Cc.isGrounded)
        {
            Anim.SetBool("WalkFr", false);
            Anim.SetBool("Jump", true);
            Deplacements.y = JumpSpeed;
        }
        else
        {
            Deplacements.y = speedY;
        }

        if (!Cc.isGrounded)
        {
            Anim.SetBool("Jump", false);
            //Adding gravity
            Deplacements.y -= Gravity * Time.deltaTime;
        }

        //Animations
        if (speedX < 0)
        {
            Anim.SetBool("WalkLf", true);
        }
        else if (speedX > 0)
        {
            Anim.SetBool("WalkRt", true);
        }
        else if (speedZ < 0)
        {
            Anim.SetBool("WalkBk", true);
        }
        else if (speedZ > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Anim.SetBool("WalkFr", false);
            }
            else
            {
                Anim.SetBool("WalkFr", true);
            }
        }
        else
        {
            Anim.SetBool("WalkRt", false);
            Anim.SetBool("WalkLf", false);
            Anim.SetBool("WalkBk", false);
            Anim.SetBool("WalkFr", false);
        }
        //Finale define where the player should go
        Cc.Move(Deplacements * Time.deltaTime);
    }
}
