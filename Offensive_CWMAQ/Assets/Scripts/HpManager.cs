using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HpManager : MonoBehaviour
{

    public int Hp;
    // Start is called before the first frame update
    void Start()
    {
        Hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(Hp <= 0 && CompareTag("Player"))
        {
            Debug.Log("oui");
            SceneManager.LoadScene("EndScene");
        }

        if (Hp <= 0 && CompareTag("Bot"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
