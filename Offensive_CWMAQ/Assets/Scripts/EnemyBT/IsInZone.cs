using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class IsInZone : Node
{
    private Transform _selfTransform;
    public IsInZone()
    {

    }

    public override NodeState Evaluate()
    {
        // get the zonewall to get its component and then the AI can get the distance between 
        GameObject ZoneWall = GameObject.Find("ZoneWall");
        float distance = ZoneWall.GetComponent<ZoneManager>().DistZone(_selfTransform.position);

        if (distance > ZoneWall.GetComponent<ZoneManager>().GetRadiusZone())
        {
            state = NodeState.SUCCESS;
            return state;
        }




        state = NodeState.FAILURE;
        return state;
    }
}
