using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class WalkAroundTask : Node
{
    private Transform _selfTransform;
    private NavMeshAgent _selfAgent;
    private Animator _selfAnimator;


    public WalkAroundTask(Transform selftransform, NavMeshAgent selfagent, Animator selfanimator)
    {
        _selfTransform = selftransform;
        _selfAgent = selfagent;
        _selfAnimator = selfanimator;
    }


    public override NodeState Evaluate()
    {
        
       
        GameObject ZoneWall = GameObject.Find("ZoneWall");

        Vector2 randPos = Random.insideUnitCircle * 100f;
        Vector3 newPos = new Vector3(_selfTransform.position.x + randPos.x, _selfTransform.position.y, _selfTransform.position.z + randPos.y);
        Vector3 NextPos = new Vector3(newPos.x + _selfTransform.forward.x, newPos.y + _selfTransform.forward.y, newPos.z + _selfTransform.forward.z);

        if (!_selfAgent.hasPath && ZoneWall.GetComponent<ZoneManager>().InNextZone(NextPos))
        {
            _selfAgent.destination = NextPos;

            _selfAnimator.SetBool("WalkFr", false);
        }

        _selfAgent.isStopped = false;


        state = NodeState.RUNNIG;
        return state;
    }
}
