using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{

    private float radius;
    private float shrinkRadius;
    private float timeToShrink = 60f;
    private float timeBeforeShrink=60f;
    private float istimeToSearch =0f;
    


    public GameObject Interior;
    public GameObject Exterior;
    private Vector3 centerposition = Vector3.zero;
    
    private GameObject ZoneWall;
    private GameObject MusicManager;
    
    private int i = 0;


    private bool DoOnceResearch = true;
    private bool IsZoneDefine = false;
    

    // Start is called before the first frame update
    void Start()
    {
        ZoneWall = GameObject.Find("ZoneWall");
        MusicManager = GameObject.Find("MusicManager");
        // set the zone wall to false //
        Interior.SetActive(false);
        Exterior.SetActive(false);
        //
        IsZoneDefine = false;
    }


    // Update is called once per frame
    void Update()
    {
        istimeToSearch += Time.deltaTime;
        timeBeforeShrink -= Time.deltaTime;
        Debug.Log(timeBeforeShrink);

        // if it's the time to create the first zone and it's not done do it //
        if (istimeToSearch > 15f && DoOnceResearch)
        {
            // Get the all the players alive//
            GameObject[] alivePlayers = GameObject.FindGameObjectsWithTag("Enemy");
            
            // for each alive player add their position to the center position then divide the center position by the number of alive player and you get the average position//
            foreach (GameObject alive in alivePlayers)
            {
                centerposition += alivePlayers[i].transform.position;
                i += 1;
            }
            centerposition = centerposition / i;
            //

            // get the empty object placed on the corner of the map //
            GameObject[] CornerMap = GameObject.FindGameObjectsWithTag("Corner");
            //

            // Organize them by the distance between the center position and the position of each corner //
            CornerMap = CornerMap.OrderBy(hit => Vector3.Distance(hit.transform.position, centerposition)).ToArray<GameObject>();
            //

            // the radius of the zone equal the distance between the furthest corner and the center position//
            radius = Vector3.Distance(CornerMap[3].transform.position, centerposition);
            //

            //set the next zone radius //
            shrinkRadius = radius / 2;
            //

            // set active the zone wall //
            Interior.SetActive(true);
            Exterior.SetActive(true);
            //
            IsZoneDefine = true;
            DoOnceResearch = false;
        }
        // set the center of the zone //
        ZoneWall.transform.position = centerposition + new Vector3(0f,-20f,0f);
        //

        // if the radius of the zone is equal to the next radius zone set a new radius to the next zone //
        if (radius-shrinkRadius<=0 && ZoneDefine())
        {
            Debug.Log("nex zone");
            timeBeforeShrink = 60;
            shrinkRadius = radius / 2;
        }
        //
        

        // if it's time to shrink the zone //

        if (timeBeforeShrink<=0f)
        { 
            radius = Mathf.MoveTowards(radius, shrinkRadius, ((shrinkRadius) / timeToShrink) * Time.deltaTime);
            MusicManager.GetComponent<MusicManager>().PlaySoundZone = true;
        }
        //

        ZoneWall.transform.localScale = new Vector3((radius*0.01f), 4, (radius*0.01f));
        
    }

    // function to get the center of the zone//
    public Vector3 GetCenterZone()
    {
        return centerposition;
    }
    //

    // function to get the next radius zone //
    public float GetNextRadiusZone()
    {
        return shrinkRadius;
    }
    //

    // function to get the distance between a position you give to the fonction and the center of the position//
    public float DistZone(Vector3 position)
    {
        return Vector3.Distance(centerposition, position);
    }
    //

    // function that return if the position given is in the next zone //
    public bool InNextZone(Vector3 position)
    {
        if (DistZone(position) < shrinkRadius)
        {
            return true;
        }

        return false;
    }
    //

    // function that return the position given is in the current zone //
    public bool InZone(Vector3 position)
    {
        if (DistZone(position) < radius) 
        {
            return true;
        }

        return false;
    }
    //
    
    // function that return if the zone is define //
    public bool ZoneDefine()
    {
        return IsZoneDefine; 
    }
    //
}
