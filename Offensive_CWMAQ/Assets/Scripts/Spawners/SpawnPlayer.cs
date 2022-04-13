using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private float posX;
    private float posZ;
    public void Start()
    {
        posX = Random.Range(50, 950);
        posZ = Random.Range(10, 450);

        transform.position = new Vector3(posX, 100, posZ);
    }   
}
