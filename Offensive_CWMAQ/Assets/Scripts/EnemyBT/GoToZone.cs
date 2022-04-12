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
       
        Vector3 centerZone = (Vector3)GetData("CenterZone");

        if (centerZone == null)
        {
            state = NodeState.FAILURE;
            return state;
        }


        _selfAgent.destination = centerZone;
        _selfAnimator.SetBool("WalkFr", true);



        state = NodeState.RUNNIG;
        return state;
    }
}
