using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HpManager : MonoBehaviour
{

    public int Hp;
    void Start()
    {
        Hp = 100;
    }

    void Update()
    {

        //This condition allow the player to lose switch scene if the player has no HP left 

        if(Hp <= 0 && CompareTag("Player"))
        {
            SceneManager.LoadScene("EndScene");
        }

        //This condition allow the bot to die if he has not hp left 

        if (Hp <= 0 && CompareTag("Bot"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
