using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class ShootEnemy : Node
{
    private float time = 0.5f;
    private float timer = 0f;
    private Transform _selfTransform;
    private Animator _selfAnimator;
    private NavMeshAgent _selfAgent;

    private AudioSource _selfAudioSource;
    private AudioClip shootSound;

    private Vector3 origin = Vector3.zero;
    private Vector3 newTargetPos = Vector3.zero;
    public ShootEnemy(Transform selftransform, Animator animator, NavMeshAgent selfagent,AudioClip shootsound,AudioSource selfaudiosource)
    {
        _selfTransform = selftransform;
        _selfAnimator = animator;
        _selfAgent = selfagent;
        _selfAudioSource = selfaudiosource;
        shootSound = shootsound;


    }

    public override NodeState Evaluate()
    {
        // the AI check if he has a target if not it's failure //
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            _selfAgent.isStopped = false;
            _selfAnimator.SetBool("Shoot", false);
            state = NodeState.FAILURE;
            return state;
        }
        //
       
        origin = new Vector3(_selfTransform.position.x, _selfTransform.position.y + 1f, _selfTransform.position.z);
        newTargetPos = new Vector3(target.position.x, target.position.y + 1f, target.position.z);

        Vector3 deltaPosition = new Vector3(target.position.x - _selfTransform.position.x, target.position.y - _selfTransform.position.y, target.position.z - _selfTransform.position.z);
        float angle = Vector3.Angle(_selfTransform.forward, deltaPosition);
        Vector3 randomshoot = Random.insideUnitSphere * 0.5f;
        Vector3 direction = newTargetPos - origin;
        Vector3 newdirection = direction + randomshoot;
        //if IA has a target raycast to him in a define range and shoot when timer equel the time to shoot//

        RaycastHit hit;
        if (Physics.Raycast(origin, newdirection, out hit, 80f) && angle < 70)
        {
            timer += Time.deltaTime;
            _selfAgent.isStopped = true;
            if (hit.transform.CompareTag("Enemy") && timer>= time)
            {
                
                _selfAudioSource.PlayOneShot(shootSound);
                _selfAnimator.SetBool("WalkFr", false);
                _selfAnimator.SetBool("Shoot", true);
                _selfTransform.LookAt(target.position);
                _selfTransform.GetComponent<AmmoManager>().Ammo -= 1;
                hit.transform.GetComponent<HpManager>().Hp -= 10;
                timer = 0;
                state = NodeState.RUNNIG;
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
            
        state = NodeState.RUNNIG;
        return state;

    }

}
