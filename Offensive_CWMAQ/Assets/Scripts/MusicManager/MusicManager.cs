using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    int i ;
    int id ;
    int ie ;
    public AudioSource DiedSound;
    public AudioClip DiedClip;
    public AudioSource WinMusic;
    public AudioClip WinClip;
    public AudioSource ShootMusic;
    public AudioClip Shootclip;
    public AudioSource ZoneMusic;
    public AudioClip Zoneclip;
    private GameObject player;
    private GameObject gameMaster;
    public bool PlayShootSound;
    public bool PlaySoundZone = false;
    private float nextFire;
    private float fireRate = 0.1f;
    private void Start()
    {
        player = GameObject.Find("Player");
        gameMaster = GameObject.Find("GameMaster");
        i = 0;
        id = 0;
        ie = 0;
    }
    private void Update()
    {
        
        if(gameMaster.GetComponent<GameMaster>().IsPlayerWin == true && i == 0)
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

            nextFire = Time.time + fireRate;
            ShootMusic.PlayOneShot(Shootclip);
        }

        if (player.GetComponent<HpManager>().PlayDeadSound == true && id == 0) 
        {
            id++;
            DiedSound.PlayOneShot(DiedClip);
        }
        else
        {
            id = 0;
        }

        if(PlaySoundZone && ie == 0)
        {
            ie++;
            ZoneMusic.PlayOneShot(Zoneclip);
        }
        else
        {
            ie = 0;
        }
    }
}
