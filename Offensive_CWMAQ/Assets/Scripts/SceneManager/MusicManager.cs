using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource MenuMusic;
    public AudioClip ClipMusicMenu;
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            MenuMusic.PlayOneShot(ClipMusicMenu);
        }
        else
        {
            MenuMusic.Stop();
        }
    }
}
