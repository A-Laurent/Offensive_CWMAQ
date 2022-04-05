using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //Définition des variables
    public float FireRate = 0.1f;
    private float nextFire;
    public float WeaponRange = 1000f;
    public Camera FpsCam;
    public AudioSource Audiosource;
    public AudioClip Sound;
    public Animator Anim;
    void Update()
    {
        //Condition pour tirer
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Anim.SetBool("Shoot", true);
            if (Time.time > nextFire)
            {
                //Temps entre chaque tire 
                nextFire = Time.time + FireRate;

                //Définition de la ou le rayon vas partir 
                Ray rayOrigin = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

                RaycastHit hit;

                //Création de ray avec une range max
                if (Physics.Raycast(rayOrigin, out hit, WeaponRange))
                {
                    //Bruit de coup de feu
                    Audiosource.PlayOneShot(Sound);
                }
            }
        }
        else
        {
            Anim.SetBool("Shoot", false);
        }
    }
}
