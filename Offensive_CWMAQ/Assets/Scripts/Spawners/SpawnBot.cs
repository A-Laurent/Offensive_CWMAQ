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
        for (int i = 0;i<99;i++) 
        {   //Random on all the map for where the bot can spawn
            posX = Random.Range(50, 950);
            posZ = Random.Range(10, 450);
            //Instantiate them with a prefab
            Instantiate(Botsprefab, new Vector3(posX, 10, posZ), Quaternion.identity);
        }
    }
}
