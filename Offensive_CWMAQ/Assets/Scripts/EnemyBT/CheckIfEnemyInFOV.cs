using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class CheckIfEnemyInFOV : Node
{
    private Transform _selfTransform;
    private NavMeshAgent _selfAgent;
    private static int _enemyToGuardLayer = 1 << 6;
    private int _maxTarget = 1;

    private Vector3 origin = Vector3.zero;
    private Vector3 newTargetPos = Vector3.zero;
    
    public CheckIfEnemyInFOV(Transform transform, NavMeshAgent selfagent)
    {
        _selfTransform = transform;
        _selfAgent = selfagent;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        
        // The Ai check Around if there are any collider that have the 6th and then put them in the right order the closer the enemy is the higher its place is in the List//
        Collider[] enemisAround = Physics.OverlapSphere(_selfTransform.position, 50f, _enemyToGuardLayer);
        //Debug.Log(enemisAround.Length);
        if (enemisAround.Length == 0)
        {
            state = NodeState.FAILURE;
            return state;
        }
        if (enemisAround.Length > _maxTarget)
        {
            enemisAround.OrderBy(hit => Vector3.Distance(hit.transform.position, _selfTransform.position)).ToArray();
        }

        
        List<Transform> enemiesToHit = new List<Transform>();

        for (int i = 0; i < _maxTarget; i++)
        {
            if (i < enemisAround.Length)
            {
                enemiesToHit.Add(enemisAround[i].transform);
            }
            else
            {
                break;
            }

        }
        //

        origin = new Vector3(_selfTransform.position.x, _selfTransform.position.y + 1f, _selfTransform.position.z);
        newTargetPos = new Vector3(enemiesToHit[0].position.x, enemiesToHit[0].position.y + 1f, enemiesToHit[0].position.z);

        // The AI calculate the deltaposition between him and the nearest enemy and the angle between The AI forward the deltaposition//
        Vector3 deltaPosition = new Vector3(enemiesToHit[0].position.x - _selfTransform.position.x, enemiesToHit[0].position.y - _selfTransform.position.y, enemiesToHit[0].position.z - _selfTransform.position.z);
        float angle = Vector3.Angle(_selfTransform.forward, deltaPosition);
        //
       
        // The Ai check if the Raycast hit the enemy only if he is in the FOV an set in the data "target" equal to the transform of the  raycast hit//
        RaycastHit hit;
        if (Physics.Raycast(origin, newTargetPos - origin, out hit, 80f)  && angle < 70.0f)
        {
            
            if (hit.transform.CompareTag("Enemy"))
            {
                ClearData("target");
                _selfAgent.isStopped = true;
                parent.parent.parent.SetData("target", hit.transform);
                state = NodeState.SUCCESS;
                return state;
            }

        }
        //

        //And if after all this check target == null no one is around so it's a failure else t == something so return success//

        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }


        state = NodeState.SUCCESS;
        return state;
        //

    }
}

