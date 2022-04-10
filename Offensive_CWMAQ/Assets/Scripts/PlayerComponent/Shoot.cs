using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float FireRate = 0.1f;
    public float nextFire;
    public float WeaponRange = 1000f;
    public CameraComp CamComp;
    public Camera TpsCam;
    private GameObject GameMaster;
    public Animator Anim;
    public bool PlayShootSound;
    private void Start()
    {
        GameMaster = GameObject.Find("GameMaster");
    }
    void Update()
    {
        RaycastHit hit;
        //Shoot with Left click
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (GetComponent<AmmoManager>().Ammo >= 1)
            {
                Anim.SetBool("Shoot", true);
                if (Time.time > nextFire)
                {
                    //Shot fire
                    PlayShootSound = true;

                    GetComponent<AmmoManager>().Ammo -= 1;
                    //Duration between two fire 
                    nextFire = Time.time + FireRate;

                    //From where the ray with start 
                    Ray rayOrigin = TpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

                    //Creation of ray with a max range
                    if (Physics.Raycast(rayOrigin, out hit, WeaponRange))
                    {
                        //If it's a bot we kill
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            hit.collider.GetComponent<HpManager>().Hp -= 10;
                            if (hit.collider.GetComponent<HpManager>().Hp <= 0)
                            {
                                GameMaster.GetComponent<GameMaster>().Kills += 1;
                            }
                        }
                    }
                }
                
            }
        }
        else
        {
            
            PlayShootSound = false;
            
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
