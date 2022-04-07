using System.Collections;
using System.Collections.Generic;
using BehaviorTree;

public class EnemyBT : Tree
{
    public UnityEngine.AI.NavMeshAgent SelfAgent;
    public UnityEngine.Animator SelfAnimator;


    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckIfEnemyInFOV(transform),
                new ShootEnemy(transform, SelfAnimator, SelfAgent),
            }),
            new GoToTarget(transform,SelfAgent),
        
        });



        return root;
    }
}
