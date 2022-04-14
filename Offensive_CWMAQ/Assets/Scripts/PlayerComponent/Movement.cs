using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour
{
    //Player
    public float NormalSpeed = 5.0f;
    public float RunSpeed = 9.0f;
    public float JumpSpeed = 6.0f;
    public float Gravity = 20.0f;
    public float delayBetweenStep;
    public bool CanMove = true;
    private float delay;

    //GameMaster
    public GameObject GameMaster;

    //CharacterController
    CharacterController cc;

    Vector3 deplacements;

    //Animation
    public Animator Anim;

    //Camera 
    public Camera HeadPlayer;
    
    void Start()
    {
        //suppr cursor
        Cursor.visible = false;
        //Get Charactere controler component
        cc = GetComponent<CharacterController>();
        delay = delayBetweenStep;
    }

    void Update()
    {
        if(CanMove)
            Movements();
    }

    private void Movements()
    {
        //Initializing movement vectors
        Vector3 z = transform.TransformDirection(Vector3.forward);
        Vector3 x = transform.TransformDirection(Vector3.right);

        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");

        //The jumping variables
        float speedY = deplacements.y;

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

        deplacements = z * speedZ + x * speedX;

        //The jump
        if (Input.GetButton("Jump") && cc.isGrounded||Input.GetButtonDown("ButtonA") &&cc.isGrounded)
        {
            Anim.SetBool("WalkFr", false);
            Anim.SetBool("Jump", true);
            deplacements.y = JumpSpeed;
        }
        else
        {
            deplacements.y = speedY;
        }

        if (!cc.isGrounded)
        {
            Anim.SetBool("Jump", false);
            //Adding gravity
            deplacements.y -= Gravity * Time.deltaTime;
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
        cc.Move(deplacements * Time.deltaTime);
    }
}
