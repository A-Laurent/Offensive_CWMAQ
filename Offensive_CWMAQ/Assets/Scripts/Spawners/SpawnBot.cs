using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnBot : MonoBehaviour
{
    private float posX;
    private float posZ;
    public GameObject Botsprefab;
    void Start()
    {
        for (int i = 0;i<98;i++) 
        {   //Random on all the map for where the bot can spawn
            posX = Random.Range(50, 950);
            posZ = Random.Range(10, 450);

            NavMeshHit hit;

            while (!NavMesh.SamplePosition(new Vector3(posX, 50, posZ), out hit, 100.0f, NavMesh.AllAreas)) ;

            //Instantiate them with a prefab
            Instantiate(Botsprefab, hit.position , Quaternion.identity);
        }
    }
}
