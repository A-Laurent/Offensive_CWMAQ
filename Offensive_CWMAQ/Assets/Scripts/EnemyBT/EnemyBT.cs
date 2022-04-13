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
        Node root = new Selector(new List<Node>
        {

            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckIfEnemyInFOV(transform, SelfAgent),
                    new ShootEnemy(transform,SelfAnimator,SelfAgent,ShootSound,SelfAudioSource),
                }),
                new GoToTarget(transform,SelfAgent,SelfAnimator),

            }),
            new Sequence(new List<Node>
            { 
                new IsInZone(transform,SelfAgent, SelfAnimator),
                new GoToZone(SelfAgent,SelfAnimator),

            }),
            new Sequence(new List<Node>
            {
                new NeedToHeal(transform),
                new GoToHeal(transform,SelfAgent,SelfAnimator),

            }),
            new Sequence(new List<Node>
            {
                new NeedToReload(transform),
                new GoToReload(transform,SelfAgent,SelfAnimator),

            }),
            new WalkAroundTask(transform,SelfAgent,SelfAnimator),
           

        });
       
        return root;
    }
}
