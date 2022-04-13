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
        
        // get the object ZoneWall//
        GameObject ZoneWall = GameObject.Find("ZoneWall");
        //

        // create a random new position //
        Vector2 randPos = Random.insideUnitCircle * 100f;
        Vector3 newPos = new Vector3(_selfTransform.position.x + randPos.x, _selfTransform.position.y, _selfTransform.position.z + randPos.y);
        Vector3 NextPos = new Vector3(newPos.x + _selfTransform.forward.x, newPos.y + _selfTransform.forward.y, newPos.z + _selfTransform.forward.z);
        //

        // set the next destination of the AI if it has not path and the nextposition is in the zone //
        if (!_selfAgent.hasPath && ZoneWall.GetComponent<ZoneManager>().InNextZone(NextPos))
        {
            _selfAgent.destination = NextPos;

            _selfAnimator.SetBool("WalkFr", true);
        }
        else if (!_selfAgent.hasPath && !ZoneWall.GetComponent<ZoneManager>().ZoneDefine()) // and if the zone is not define go where the AI want //
        {

            _selfAgent.destination = NextPos;
            _selfAnimator.SetBool("WalkFr", true);
        }
        //

        _selfAgent.isStopped = false;


        state = NodeState.RUNNIG;
        return state;
    }
}
