using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class EscapeTask : Node
{

    private Transform _selfTransform;
    private NavMeshAgent _selfAgent;



    public EscapeTask(Transform selftransform, NavMeshAgent selfAgent)
    {
        _selfTransform = selftransform;
        _selfAgent = selfAgent;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target = null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        
        
        
        
        return state;
    }
}
