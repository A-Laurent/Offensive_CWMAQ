using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class IsInZone : Node
{
    private Transform _selfTransform;
    public IsInZone(Transform selftransform)
    {
        _selfTransform = selftransform;
    }

    public override NodeState Evaluate()
    {
        // get the zone wall object//
        GameObject ZoneWall = GameObject.Find("ZoneWall");
        //

        //if there is no zonewall return failure //
        if ( ZoneWall == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        //
        // get the distance between the position and the Ai and the center of the zone//
        float distance = ZoneWall.GetComponent<ZoneManager>().DistZone(_selfTransform.position);
        //

        // if the distance is longer than th radius of the next zone it return success//
        if (distance > ZoneWall.GetComponent<ZoneManager>().GetNextRadiusZone())
        {
            parent.SetData("CenterZone", ZoneWall.GetComponent<ZoneManager>().GetCenterZone());
            state = NodeState.SUCCESS;
            return state;
        }
       

        //
        
        state = NodeState.FAILURE;
        return state;
    }
}
