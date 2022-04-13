using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class IsInZone : Node
{
    private Transform _selfTransform;
    private NavMeshAgent _selfAgent;
    private Animator _selfAnimator;
    public IsInZone(Transform selftransform, NavMeshAgent selfagent, Animator selfanimator)
    {
        _selfTransform = selftransform;
        _selfAgent = selfagent;
        _selfAnimator = selfanimator;
    }

    public override NodeState Evaluate()
    {
        
        // get the zone wall object//
        GameObject ZoneWall = GameObject.Find("ZoneWall");
        //
       
        //if there is no zonewall return failure //
        if ( ZoneWall == null || ZoneWall.GetComponent<ZoneManager>().GetCenterZone() == Vector3.zero)
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
            parent.SetData("CenterZone",ZoneWall.GetComponent<ZoneManager>().GetCenterZone());
            _selfAgent.isStopped = false;
            state = NodeState.SUCCESS;
            return state;
        }
        if (ZoneWall.GetComponent<ZoneManager>().InNextZone(_selfTransform.position))
        {

            _selfAnimator.SetBool("WalkFr", false);
            _selfAgent.isStopped = true;
            ClearData("CenterZone");
            state = NodeState.FAILURE;
            return state;
        }

        //
        
        state = NodeState.FAILURE;
        return state;
    }
}
