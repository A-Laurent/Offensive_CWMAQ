using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    int Items;
    private GameObject[] spawners;
    public GameObject MedKitprefab;
    public GameObject Ammoprefab;
    void Start()
    {
        int i = 0;
        //I'll take all the position of my empty that I place
        spawners = GameObject.FindGameObjectsWithTag("ItemPos");
        foreach (GameObject spawner in spawners)
        {
            //Give them a rand with an Ammo and a Medkit
            Items = Random.Range(1, 3);
            //Them Instantiate them
            if (Items == 1)
            {
                Instantiate(MedKitprefab, spawners[i].transform.position, Quaternion.identity);
            }

            if (Items == 2)
            {
                Instantiate(Ammoprefab, spawners[i].transform.position, Quaternion.identity);
            }

            i += 1;
        }        
    }
}
