using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class ShootEnemy : Node
{
    private float time = 3f;
    private float timer = 0f;
    private Transform _selfTransform;
    public ShootEnemy(Transform selftransform)
    {
        _selfTransform = selftransform;
    }

    public override NodeState Evaluate()
    {
        // the AI check if he has a target if not it's failure //
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        //

        //if IA has a target raycast to him in a define range and shoot when timer equel the time to shoot//
        RaycastHit hit;
        if (Physics.Raycast(_selfTransform.position,target.position - _selfTransform.position, out hit, 20f))
        {
            timer += Time.deltaTime;
            if (hit.transform.CompareTag("Enemy") && timer>= time)
            {
                Debug.Log("pan");
                //deal damage//
                timer = 0;
            }
            
        }
        //

        // while he can shoot it's running//
        state = NodeState.RUNNIG;
        return state;
    }

}
