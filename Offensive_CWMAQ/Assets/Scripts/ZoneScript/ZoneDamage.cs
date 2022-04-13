using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDamage : MonoBehaviour
{
    private GameObject ZoneWall;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        ZoneWall = GameObject.Find("ZoneWall");
        timer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ZoneWall.GetComponent<ZoneManager>().InZone(transform.position) && ZoneWall.GetComponent<ZoneManager>().ZoneDefine())
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                this.GetComponent<HpManager>().Hp -= 1;
                timer = 1f;
            }
        }
    }
}
