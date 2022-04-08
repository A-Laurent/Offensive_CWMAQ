using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class GoToZone : Node
{

    private NavMeshAgent _selfAgent;
    public GoToZone(NavMeshAgent selfagent)
    {
        _selfAgent = selfagent;
    }

    public override NodeState Evaluate()
    {
        Vector3 centerZone = (Vector3)GetData("CenterZone");

        if (centerZone == null)
        {
            state = NodeState.FAILURE;
            return state;
        }


        _selfAgent.destination = centerZone;



        state = NodeState.RUNNIG;
        return state;
    }
}
