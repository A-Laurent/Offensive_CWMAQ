using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDamage : MonoBehaviour
{
    private GameObject ZoneWall;
    // Start is called before the first frame update
    void Start()
    {
        ZoneWall = GameObject.Find("ZoneWall");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ZoneWall.GetComponent<ZoneManager>().InZone(transform.position))
        {
            this.GetComponent<HpManager>().Hp -= 1;
        }
    }
}
