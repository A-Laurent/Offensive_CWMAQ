using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    public AudioSource DiedSound;
    public AudioClip DiedClip;
    private GameObject Player;
    private GameObject GameMaster;
    public int Hp;
    int i = 0;
    void Start()
    {
        Hp = 100;
        Player = GameObject.Find("Player");
        GameMaster = GameObject.Find("GameMaster");
    }

    void Update()
    {

        //This condition allow the player to lose switch scene if the player has no HP left 

        if(Hp <= 0 && GetComponent<Movement>()&& i == 0)
        {
            //GameMaster.GetComponent<GameMaster>().CanMove = false;
            GameMaster.GetComponent<GameMaster>().IsPlayerDead = true;
            DiedSound.PlayOneShot(DiedClip);
            i ++;
        }

        //This condition allow the bot to die if he has not hp left 
        
        if (Hp <= 0 && transform.CompareTag("Enemy"))
        {
            GameObject.Destroy(this.gameObject);                     
        }
    }

}
