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
            //Je multiplie simplement la vitesse x et z
            speedX = speedX * RunSpeed;
            speedZ = speedZ * RunSpeed;
        }
        else
        {
            speedX = speedX * NormalSpeed;
            speedZ = speedZ * NormalSpeed;
        }

        Deplacements = z * speedZ + x * speedX;

        //Ma condition pour le saut
        if (Input.GetButton("Jump") && Cc.isGrounded)
        {
            //Je donne un float en deplacement y 
            Deplacements.y = JumpSpeed;
        }
        else
        {
            Deplacements.y = speedY;
        }

        if (!Cc.isGrounded)
        {
            //Je rajoute ma gravité a mon CC
            Deplacements.y -= Gravity * Time.deltaTime;
        }

        //Définition finale de l'emplacement vers le quelle mon personnage dois aller
        Cc.Move(Deplacements * Time.deltaTime);

        //La rotation de la camera avec une sencibilitée
        rotationX += -Input.GetAxis("Mouse Y") * Sensivity;

        //Rotation maximum et minimun vers le haut et le bas
        rotationX = Mathf.Clamp(rotationX, -LimitRotation, LimitRotation);

        //Transformation de la postion de la cam
        HeadPlayer.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        //Transformation de la ou regarde le personnage 
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * Sensivity, 0);

        //Augementation de la précision
        //Ray ray = HeadPlayer.ViewportPointToRay(new Vector3(.5f, .5f));
    }
}
