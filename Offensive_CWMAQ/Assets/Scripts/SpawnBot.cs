using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBot : MonoBehaviour
{
    private float posX;
    private float posZ;
    public GameObject Botsprefab;
    void Start()
    {
        for (int i = 0;i<100;i++) 
        {   //Random on all the map for where the bot can spawn
            posX = Random.Range(0, 1000);
            posZ = Random.Range(0, 500);
            //Instantiate them with a prefab
            Instantiate(Botsprefab, new Vector3(posX, 10, posZ), Quaternion.identity);
        }
    }
}
