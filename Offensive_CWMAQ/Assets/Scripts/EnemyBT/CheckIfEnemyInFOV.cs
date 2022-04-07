using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class CheckIfEnemyInFOV : Node
{
    private Transform _transform;
    private static int _enemyToGuardLayer = 1 << 6;
    private int _maxTarget = 1;

    private Vector3 origin = Vector3.zero;
    private Vector3 newTargetPos = Vector3.zero;
    
    public CheckIfEnemyInFOV(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        // The Ai check Around if there are any collider that have the 6th and then put them in the right order the closer the enemy is the higher its place is in the List//
        Collider[] enemisAround = Physics.OverlapSphere(_transform.position, 40f, _enemyToGuardLayer);
        
        
        if (enemisAround.Length == 0)
        {
            state = NodeState.FAILURE;
            return state;
        }
        if (enemisAround.Length > _maxTarget)
        {
            enemisAround.OrderBy(hit => Vector3.Distance(hit.transform.position, _transform.position)).ToArray();
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

        origin = new Vector3(_transform.position.x, _transform.position.y + 1f, _transform.position.z);
        newTargetPos = new Vector3(enemiesToHit[0].position.x, enemiesToHit[0].position.y + 1f, enemiesToHit[0].position.z);

        // The AI calculate the deltaposition between him and the nearest enemy and the angle between The AI forward the deltaposition//
        Vector3 deltaPosition = new Vector3(enemiesToHit[0].position.x - _transform.position.x, enemiesToHit[0].position.y - _transform.position.y, enemiesToHit[0].position.z - _transform.position.z);
        float angle = Vector3.Angle(_transform.forward, deltaPosition);
        //
       
        // The Ai check if the Raycast hit the enemy only if he is in the FOV an set in the data "target" equal to the transform of the  raycast hit//
        RaycastHit hit;
        if (Physics.Raycast(origin, newTargetPos - origin, out hit, 30f) ) //&& angle < 60.0f
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                ClearData("target");

                parent.parent.SetData("target", hit.transform);
                Debug.Log(GetData("target"));
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

