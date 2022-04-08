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
    CharacterController Cc;
    Vector3 Deplacements;
    public Animator Anim;
    public AudioSource WalkSound;
    public AudioClip WalkClip;

    //Camera 
    public Camera HeadPlayer;
    
    void Start()
    {
        //suppr cursor
        Cursor.visible = false;
        //Get Charactere controler component
        Cc = GetComponent<CharacterController>();
    }

    void Update()
    {
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
        float speedY = Deplacements.y;

        //The sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Anim.SetBool("WalkFr", false);
            Anim.SetBool("Run", true);
            speedX = speedX * RunSpeed;
            speedZ = speedZ * RunSpeed;
        }
        else
        {
            speedX = speedX * NormalSpeed;
            speedZ = speedZ * NormalSpeed;
            Anim.SetBool("Run", false);
            Anim.SetBool("WalkFr", true);
        }

        Deplacements = z * speedZ + x * speedX;

        //The jump
        if (Input.GetButton("Jump") && Cc.isGrounded)
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
        //Walking sound effect
        float timer = 0f;
        timer -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (timer <= 0)
            {
                WalkSound.PlayOneShot(WalkClip);
                timer += 10f;
            }   
        }

        //Finale define where the player should go
        Cc.Move(Deplacements * Time.deltaTime);
    }
}
