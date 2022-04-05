using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    //Définition des variables utilisées pour les mouvements du player

    public float NormalSpeed = 5.0f;
    public float RunSpeed = 9.0f;
    public float JumpSpeed = 6.0f;
    public float Gravity = 20.0f;
    private bool run = false;
    CharacterController Cc;
    Vector3 Deplacements;
    public Animator Anim;

    //Définition des variables utilisées pour la rotation de la camera 

    private float rotationX = 0.0f;
    public float Sensivity = 10.0f;
    public float LimitRotation = 60.0f; 
    public Camera HeadPlayer;
    
    void Start()
    {
        //suppréssion du curseur
        Cursor.visible = false;
        //Permet a ma variable d'accquérir le component Charactere controller
        Cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Initialisation des deux Vecteurs de mouvement
        Vector3 z = transform.TransformDirection(Vector3.forward);
        Vector3 x = transform.TransformDirection(Vector3.right);

        //Les deux axes que je définie dans les parametre de Unity
        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");

        //Et l'axe y que j'utiliserais pour le saut
        float speedY = Deplacements.y;

        //Ma condition pour faire un sprint 
        if (Input.GetKey(KeyCode.LeftShift))
            run = true;
        else run = false;

        if (run)
        { 
            Anim.SetBool("Run", true);
            //Je multiplie simplement la vitesse x et z
            speedX = speedX * RunSpeed;
            speedZ = speedZ * RunSpeed;  
        }
        else
        {
            speedX = speedX * NormalSpeed;
            speedZ = speedZ * NormalSpeed;
            Anim.SetBool("Run", false);
        }

        Deplacements = z * speedZ + x * speedX;

        //Ma condition pour le saut
        if (Input.GetButton("Jump") && Cc.isGrounded)
        {
            Anim.SetBool("Jump", true);
            //Je donne un float en deplacement y 
            Deplacements.y = JumpSpeed;
        }
        else
        {      
            Deplacements.y = speedY;
        }

        if (!Cc.isGrounded)
        {
            Anim.SetBool("Jump", false);
            //Je rajoute ma gravité a mon CC
            Deplacements.y -= Gravity * Time.deltaTime;
        }

        if(speedX<0)
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
            Anim.SetBool("WalkFr", true);
        }
        else
        {
            Anim.SetBool("WalkRt", false);
            Anim.SetBool("WalkLf", false);
            Anim.SetBool("WalkBk", false);
            Anim.SetBool("WalkFr", false);
        }

        //Définition finale de l'emplacement vers le quelle mon personnage dois aller
        Cc.Move(Deplacements * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0,HeadPlayer.gameObject.GetComponent<CameraComp>().Yaxe,0);
    }
}
