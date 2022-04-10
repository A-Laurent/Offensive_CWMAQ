using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class GoToHeal : Node
{

    private Transform _selfTransform;
    private NavMeshAgent _selfAgent;

    private float timer = 3f;
    
    public GoToHeal(Transform selftransform, NavMeshAgent selfagent)
    {
        _selfTransform = selftransform;
        _selfAgent = selfagent;
    }

    public override NodeState Evaluate()
    {
        // get the transform of the medikit //
        Transform medikit = (Transform)GetData("Medikit");
        //

        // if th emedikit is null then go to failure //
        if (medikit == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        //

        // if there is a medikit then go to its position//
        _selfAgent.destination = medikit.position;
        //

        //if the AI is close to the destination then it stopppe the time to take the heal //
        if (Vector3.Distance(_selfTransform.position,_selfAgent.destination)<0.5f)
        {
            _selfAgent.isStopped = true;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                // when the AI is healed it can go and it clear the data "Medikit" to have a failure in NeedToHeal//
                _selfAgent.isStopped = false;
                ClearData("Medikit");
                //
                //state = NodeState.FAILURE;
                //return state;
            }
        }

        state = NodeState.RUNNIG;
        return state;
    }
}
