using System.Collections;
using System.Collections.Generic;
using BehaviorTree;

public class EnemyBT : Tree
{
    protected override Node SetupTree()
    {
        Node root = new CheckIfEnemyInFOV(transform);
        return root;
    }
}
