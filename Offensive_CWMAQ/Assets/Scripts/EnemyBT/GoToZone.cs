using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class GoToZone : Node
{

    private NavMeshAgent _selfAgent;
    private Animator _selfAnimator;
    public GoToZone(NavMeshAgent selfagent, Animator selfanimator)
    {
        _selfAgent = selfagent;
        _selfAnimator = selfanimator;
    }

    public override NodeState Evaluate()
    {
       // get the center of the zone//
        Vector3 centerZone = (Vector3)GetData("CenterZone");
        //
        // if the date center of the zone is null then return failure //
        if (centerZone == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        // got to the center of the zone and set the walk animation to true //
        _selfAgent.destination = centerZone;
        _selfAnimator.SetBool("WalkFr", true);
        //


        state = NodeState.RUNNIG;
        return state;
    }
}
