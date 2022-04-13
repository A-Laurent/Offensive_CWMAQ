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
    private GameObject MusicManager;
    private void Start()
    {
        GameMaster = GameObject.Find("GameMaster");
        MusicManager = GameObject.Find("MusicManager");
    }
    void Update()
    {

        if (GameMaster.GetComponent<GameMaster>().IsPlayerDead || GameMaster.GetComponent<GameMaster>().IsPlayerWin)
            return;
        Debug.Log(Input.GetAxis("TriggerRT"));
        RaycastHit hit;
        //Shoot with Left click
        if (Input.GetKey(KeyCode.Mouse0)|| Input.GetAxis("TriggerRT") > 0.2)
        {
            if (GetComponent<AmmoManager>().Ammo >= 1)
            {
                Anim.SetBool("Shoot", true);
                if (Time.time > nextFire)
                {
                    //Shot fire
                    MusicManager.GetComponent<MusicManager>().PlayShootSound = true;

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

            MusicManager.GetComponent<MusicManager>().PlayShootSound = false;
            
            //Animation
            Anim.SetBool("Shoot", false);
        }
        //Aiming if we use Right click
        if (Input.GetKey(KeyCode.Mouse1) || Input.GetAxis("TriggerLT")>0.2)
        {
            CamComp.GetComponent<CameraComp>().Aiming = true;
        }
        else
        {
            CamComp.GetComponent<CameraComp>().Aiming = false;
        }
    }
}
