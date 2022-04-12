using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisionInZone : MonoBehaviour
{
    public Image Vision;
    private GameObject ZoneWall;

    // Start is called before the first frame update
    void Start()
    {
        ZoneWall = GameObject.Find("ZoneWall");
        Vision.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ZoneWall.GetComponent<ZoneManager>().InZone(transform.position))
        {
            Vision.gameObject.SetActive(false);
        }
        else
        {
            Vision.gameObject.SetActive(true);
        }
    }
}
