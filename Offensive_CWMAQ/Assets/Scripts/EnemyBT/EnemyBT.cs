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
                new IsInZone(transform,SelfAgent),
                new GoToZone(SelfAgent)

            }),
            new Sequence(new List<Node>
            {
                new NeedToHeal(transform),
                new GoToHeal(transform,SelfAgent),

            }),
            new Sequence(new List<Node>
            {
                new NeedToReload(transform),
                new GoToReload(transform,SelfAgent),

            }),
            new WalkAroundTask(transform,SelfAgent),
           

        });

        return root;
    }
}
