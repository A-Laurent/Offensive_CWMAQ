using System.Collections;
using System.Collections.Generic;
using BehaviorTree;

public class EnemyBT : Tree
{
    public UnityEngine.AI.NavMeshAgent SelfAgent;
    public UnityEngine.Animator SelfAnimator;
    public UnityEngine.AudioSource SelfAudioSource;
    public UnityEngine.AudioClip ShootSound;

    protected override Node SetupTree()
    {
        //Node root = new Selector(new List<Node>
        //{
        //    new Sequence(new List<Node>
        //    {
        //        new CheckIfEnemyInFOV(transform),
        //        new ShootEnemy(transform, SelfAnimator, SelfAgent,ShootSound,SelfAudioSource),
        //    }),
        //    new GoToTarget(transform,SelfAgent,SelfAnimator),
        
        //});



        return root;
    }
}
