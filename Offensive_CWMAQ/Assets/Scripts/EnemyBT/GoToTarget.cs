using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class GoToTarget : Node
{

    private Transform _selfTransform;
    private NavMeshAgent _selfAgent;

    private Vector3 lastTargetpos = Vector3.zero;

   
   public GoToTarget(Transform selftransform, NavMeshAgent selfagent)
   {
        _selfTransform = selftransform;
        _selfAgent = selfagent;
   }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Vector3 deltaPosition = new Vector3(target.position.x - _selfTransform.position.x, target.position.y - _selfTransform.position.y, target.position.z - _selfTransform.position.z);
        float distance = Vector3.Distance(target.position, _selfTransform.position);
        float angle = Vector3.Angle(_selfTransform.forward, deltaPosition);

        RaycastHit hit;
        if (Physics.Raycast(_selfTransform.position,target.position - _selfTransform.position, out hit) && angle < 90f && distance < 40f)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                lastTargetpos = target.position;
            }
        }
        else
        {
            _selfAgent.destination = lastTargetpos;
            if (!_selfAgent.hasPath)
            {
                state = NodeState.FAILURE;
                return state;
            }
        }
        state = NodeState.RUNNIG;
        return state;
    }
}
