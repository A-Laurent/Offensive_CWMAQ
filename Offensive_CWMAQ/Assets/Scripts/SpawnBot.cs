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
        { 
            posX = Random.Range(0, 1000);
            posZ = Random.Range(0, 500);
            Instantiate(Botsprefab, new Vector3(posX, 100, posZ), Quaternion.identity);
        }
    }
}
