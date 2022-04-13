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
        //Get ZoneWall to have access to CenterZone + set offset to 1000
        ZoneWall = GameObject.Find("ZoneWall");
        offset.y = 1000;
    }
    void Update()
    {
        //Get centerZone
        Vector3 centerZone = ZoneWall.GetComponent<ZoneManager>().GetCenterZone();


        //print direction point only if center zone is set
        if (centerZone == Vector3.zero)
            img.gameObject.SetActive(false); 
        else
            img.gameObject.SetActive(true);


        //Those four lines permit to the waypoint to not leave the screen
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        //Set to pos the position where i want to place the waypoint
        Vector2 pos = Camera.main.WorldToScreenPoint(centerZone + offset);

        if(Vector3.Dot((centerZone - player.transform.position), player.transform.forward) < 0)
        {
            //Target behind the player
            if (pos.x < Screen.width / 2)
                pos.x = maxX;
            else
                pos.x = minX;
        }

        //Those tree lines just apply this new position restriction to our image
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;

        //Print to the UI, distance between player and Waypoint/center of zone
        meter.text = ((int)Vector3.Distance(centerZone, player.transform.position)).ToString() + "m";
    }
}
