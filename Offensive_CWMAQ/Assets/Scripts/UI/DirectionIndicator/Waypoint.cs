using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public Image img;

    public GameObject player;
    private GameObject ZoneWall;

    public Text meter;

    public Vector3 offset;

    private void Start()
    {
        ZoneWall = GameObject.Find("ZoneWall");
        offset.y = 1000;
    }
    void Update()
    {
        Vector3 centerZone = ZoneWall.GetComponent<ZoneManager>().GetCenterZone();

        if (centerZone == Vector3.zero)
            img.gameObject.SetActive(false);
        else
            img.gameObject.SetActive(true);


        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(centerZone + offset);

        if(Vector3.Dot((centerZone - player.transform.position), player.transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
                pos.x = maxX;
            else
                pos.x = minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meter.text = ((int)Vector3.Distance(centerZone, player.transform.position)).ToString() + "m";
    }
}
