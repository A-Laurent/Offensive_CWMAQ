using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{

    private float radius;
    private float shrinkRadius;
    private float timeToShrink = 5f;
    private float timeBeforeShrink=10f;
    private float istimeToSearch =0f;
    


    public GameObject a;
    public GameObject b;
    private Vector3 centerposition = Vector3.zero;
    
    private GameObject ZoneWall;
    
    private int i = 0;


    private bool DoOnceResearch = true;

    

    // Start is called before the first frame update
    void Start()
    {
        ZoneWall = GameObject.Find("ZoneWall");
        a.SetActive(false);
        b.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        istimeToSearch += Time.deltaTime;
        timeBeforeShrink -= Time.deltaTime;

        if (istimeToSearch > 2f && DoOnceResearch)
        {
            GameObject[] alivePlayers = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject alive in alivePlayers)
            {
                if (i > 3)
                {
                    i = 3;
                }

                
                centerposition += alivePlayers[i].transform.position;
                i += 1;
            }
            centerposition = centerposition / i;

            GameObject[] CornerMap = GameObject.FindGameObjectsWithTag("Corner");

            CornerMap = CornerMap.OrderBy(hit => Vector3.Distance(hit.transform.position, centerposition)).ToArray<GameObject>();


            radius = Vector3.Distance(CornerMap[3].transform.position, centerposition);

            shrinkRadius = radius / 2;
            a.SetActive(true);
            b.SetActive(true);
            DoOnceResearch = false;
        }
        ZoneWall.transform.position = centerposition;

        if (radius-shrinkRadius<=0)
        {
            timeBeforeShrink = 10f;
            shrinkRadius = radius / 2;
        }

        

        if (timeBeforeShrink<=0f)
        { 
            radius = Mathf.MoveTowards(radius, shrinkRadius, ((shrinkRadius) / timeToShrink) * Time.deltaTime);
        }


        ZoneWall.transform.localScale = new Vector3((radius*0.01f), 1, (radius*0.01f));
        
    }
}
