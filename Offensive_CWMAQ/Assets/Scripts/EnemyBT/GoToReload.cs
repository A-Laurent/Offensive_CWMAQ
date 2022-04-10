using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class GoToReload : Node
{
    private Transform _selfTransform;
    private NavMeshAgent _selfAgent;

    private float timer;


   public GoToReload(Transform selftransform, NavMeshAgent selfagent)
    {
        _selfTransform = selftransform;
        _selfAgent = selfagent;
    }

    public override NodeState Evaluate()
    {
        // get the transform of the ammo //
        Transform ammo = (Transform)GetData("Ammo");
        //

        // if the ammo is null return failure because can't go//
        if (ammo == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        // if there is an ammo then go to its position//
        _selfAgent.destination = ammo.position;

        // if the AI is close to the destination stop the movement and wait few second to reload //
        if (Vector3.Distance(_selfTransform.position, _selfAgent.destination)<0.5f)
        {
            _selfAgent.isStopped = true;
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                // tell the AI it can go and clear the data "Ammo to have the NeedTOHEal in failure//
                _selfAgent.isStopped = false;
                ClearData("Ammo");
                //state = NodeState.FAILURE;
                //return state;

            }
        }


        state = NodeState.RUNNIG;
        return state;
    }
}
