using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class ShootEnemy : Node
{
    private float time = 3f;
    private float timer = 0f;
    private Transform _selfTransform;
    private Animator _selfAnimator;
    private NavMeshAgent _selfAgent;
    public ShootEnemy(Transform selftransform, Animator animator, NavMeshAgent selfagent)
    {
        _selfTransform = selftransform;
        _selfAnimator = animator;
        _selfAgent = selfagent;
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
        Debug.Log(target);
        //if IA has a target raycast to him in a define range and shoot when timer equel the time to shoot//
        RaycastHit hit;
        if (Physics.Raycast(_selfTransform.position,target.position - _selfTransform.position, out hit, 30f))
        {
            timer += Time.deltaTime;
            if (hit.transform.CompareTag("Enemy") && timer>= time)
            {
                _selfAgent.isStopped = true;
                _selfAnimator.SetBool("Shoot", true);
                hit.transform.GetComponent<HpManager>().Hp -= 10;
                timer = 0;
            }
            else
            {
                _selfAgent.isStopped = false;
                _selfAnimator.SetBool("Shoot", false);
                state = NodeState.FAILURE;
                return state;
            }
            
            
        }
        else
        {
            _selfAgent.isStopped = false;
            _selfAnimator.SetBool("Shoot", false);
            state = NodeState.FAILURE;
            return state;
        }
        //

        // while he can shoot it's running//
        state = NodeState.RUNNIG;
        return state;
    }

}
