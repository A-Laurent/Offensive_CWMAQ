using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float FireRate = 0.1f;
    private float nextFire;
    public float WeaponRange = 1000f;
    public CameraComp CamComp;
    public Camera TpsCam;
    public AudioSource Audiosource;
    public AudioClip Sound;
    public Animator Anim;
    void Update()
    {
        RaycastHit hit;
        //Shoot with Left click
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Anim.SetBool("Shoot", true);
            if (Time.time > nextFire)
            {
                //Duration between two fire 
                nextFire = Time.time + FireRate;

                //From where the ray with start 
                Ray rayOrigin = TpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));               

                //Creation of ray with a max range
                if (Physics.Raycast(rayOrigin, out hit, WeaponRange))
                {
                    //Shot fire
                    Audiosource.PlayOneShot(Sound);
                    //If it's a bot we kill
                    if(hit.collider.CompareTag("Enemy"))
                    {
                        hit.collider.GetComponent<HpManager>().Hp -= 10;
                    }
                }
            }
        }
        else
        {
            //Animation
            Anim.SetBool("Shoot", false);
        }
        //Aiming if we use Right click
        if(Input.GetKey(KeyCode.Mouse1))
        {
            CamComp.GetComponent<CameraComp>().Aiming = true;
        }
        else
        {
            CamComp.GetComponent<CameraComp>().Aiming = false;
        }
    }
}
