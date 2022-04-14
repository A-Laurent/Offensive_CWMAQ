using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float FireRate = 0.1f;
    public float NextFire;
    public float WeaponRange = 1000f;
    public CameraComp CamComp;
    public Camera TpsCam;
    public Animator Anim;
    private GameObject gameMaster;
    private GameObject musicManager;
    private void Start()
    {
        gameMaster = GameObject.Find("GameMaster");
        musicManager = GameObject.Find("MusicManager");
    }
    void Update()
    {

        if (gameMaster.GetComponent<GameMaster>().IsPlayerDead || gameMaster.GetComponent<GameMaster>().IsPlayerWin)
            return;
        RaycastHit hit;
        //Shoot with Left click
        if (Input.GetKey(KeyCode.Mouse0)|| Input.GetAxis("TriggerRT") > 0.2)
        {
            if (GetComponent<AmmoManager>().Ammo >= 1)
            {
                Anim.SetBool("Shoot", true);
                if (Time.time > NextFire)
                {
                    //Shot fire
                    musicManager.GetComponent<MusicManager>().PlayShootSound = true;

                    GetComponent<AmmoManager>().Ammo -= 1;
                    //Duration between two fire 
                    NextFire = Time.time + FireRate;

                    //From where the ray with start 
                    Ray rayOrigin = TpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

                    //Creation of ray with a max range
                    if (Physics.Raycast(rayOrigin, out hit, WeaponRange))
                    {
                        //If it's a bot we kill
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            hit.collider.GetComponent<HpManager>().Hp -= 10;
                            if (hit.collider.GetComponent<HpManager>().Hp <= 0)
                            {
                                gameMaster.GetComponent<GameMaster>().Kills += 1;
                            }
                        }
                    }
                }
                
            }
        }
        else
        {

            musicManager.GetComponent<MusicManager>().PlayShootSound = false;
            
            //Animation
            Anim.SetBool("Shoot", false);
        }
        //Aiming if we use Right click
        if (Input.GetKey(KeyCode.Mouse1) || Input.GetAxis("TriggerLT")>0.2)
        {
            CamComp.GetComponent<CameraComp>().Aiming = true;
            CamComp.GetComponent<CameraComp>().Sensitivity = 2.0f;
        }
        else
        {
            CamComp.GetComponent<CameraComp>().Aiming = false;
            CamComp.GetComponent<CameraComp>().Sensitivity = 4.0f;
        }
    }
}
