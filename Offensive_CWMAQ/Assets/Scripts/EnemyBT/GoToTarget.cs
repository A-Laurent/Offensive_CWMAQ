using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class GoToTarget : Node
{

    private Transform _selfTransform;
    private NavMeshAgent _selfAgent;
    private Animator _selfAnimator;

    private Vector3 lastTargetpos = Vector3.zero;
    private Vector3 origin = Vector3.zero;
    private Vector3 newTargetPos = Vector3.zero;

    public GoToTarget(Transform selftransform, NavMeshAgent selfagent, Animator selfanimator)
   {
        _selfTransform = selftransform;
        _selfAgent = selfagent;
        _selfAnimator = selfanimator;
   }

    public override NodeState Evaluate()
    {
        // get the transform of the target //
        Transform target = (Transform)GetData("target");

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        //

        // the Ai calculates the delta position between itself and the target position, the distance and the angle//
        Vector3 deltaPosition = new Vector3(target.position.x - _selfTransform.position.x, target.position.y - _selfTransform.position.y, target.position.z - _selfTransform.position.z);
        float distance = Vector3.Distance(target.position, _selfTransform.position);
        float angle = Vector3.Angle(_selfTransform.forward, deltaPosition);
        //

        // then make th position of the orgin of the raycast higher to not hit the ground //
        origin = new Vector3(_selfTransform.position.x, _selfTransform.position.y + 1f, _selfTransform.position.z);
        newTargetPos = new Vector3(target.position.x, target.position.y + 1f, target.position.z);
        RaycastHit hit;
        if (Physics.Raycast(origin, newTargetPos - origin, out hit) && angle < 70f && distance < 35f)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                lastTargetpos = target.position;
            }
        }
        else
        {   // if the Ai is not on the last position of the target go to the last position of the target //
            if (lastTargetpos != Vector3.zero && Vector3.Distance(lastTargetpos, _selfTransform.position) > 1f)
            {
                _selfAgent.destination = lastTargetpos;

            }
            // if the AI has a path set the walk animation //
            if (_selfAgent.hasPath)
            {
                _selfAnimator.SetBool("WalkFr", true);
            }
            //
            // if the AI is at the last position of the target stop the animation and return failure and clear the target //
            if (Vector3.Distance(_selfAgent.destination, _selfTransform.position) < 1f)
            {
                _selfAnimator.SetBool("WalkFr", false);
                ClearData("target");
                state = NodeState.FAILURE;
                return state;
            }
            //
        }
        state = NodeState.RUNNIG;
        return state;
    }
}
