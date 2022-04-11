using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    public AudioSource MenuSource;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == ("MenuScene"))
        {
            MenuSource.Play();
        }
        else
        {
            MenuSource.Stop();
        }
    }
    void Update()
    {
        
    }
}
