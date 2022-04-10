using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    int i ;
    int id ;
    public AudioSource DiedSound;
    public AudioClip DiedClip;
    public AudioSource WinMusic;
    public AudioClip WinClip;
    public AudioSource ShootMusic;
    public AudioClip Shootclip;
    private GameObject Player;
    private GameObject GameMaster;
    public bool PlayShootSound;
    private float nextFire;
    private float FireRate = 0.1f;
    private void Start()
    {
        Player = GameObject.Find("Player");
        GameMaster = GameObject.Find("GameMaster");
        i = 0;
        id = 0;
    }
    private void Update()
    {
        
        if(GameMaster.GetComponent<GameMaster>().IsPlayerWin == true && i == 0)
        {
            i++;
            WinMusic.PlayOneShot(WinClip);
        }
        else
        {
            i = 0;
        }

        if (PlayShootSound == true && Time.time >nextFire)
        {

            nextFire = Time.time + FireRate;
            ShootMusic.PlayOneShot(Shootclip);
        }

        if (Player.GetComponent<HpManager>().PlayDeadSound == true && id == 0) 
        {
            id++;
            DiedSound.PlayOneShot(DiedClip);
        }
        else
        {
            id = 0;
        }
    }
}
