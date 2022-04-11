using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

public class NeedToHeal : Node
{
    private Transform _selfTransform;

    private int _layerMedikit = 1 << 8;
    public NeedToHeal(Transform selftransform)
    {
        _selfTransform = selftransform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("Medikit");

        // check if there is collider with the layer medikit, if there is get them //
        Collider[] collidermedikits = Physics.OverlapSphere(_selfTransform.position, 80f, _layerMedikit);
        //

        // if there is no medikit around then got o failure//
        if (collidermedikits.Length == 0)
        {
            state = NodeState.FAILURE;
            return state;
        }
        //

        // if there are more than one medikit organize them by the distance between them and th AI//
        if (collidermedikits.Length > 1)
        {
            collidermedikits.OrderBy(hit => Vector3.Distance(hit.transform.position, _selfTransform.position)).ToArray();
        }
        //

        // if the date is null return failure//
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        //

        // if the AI need to heal set themedikit to data and return success//
        if (_selfTransform.GetComponent<HpManager>().Hp < 50)
        {
            
            parent.SetData("Medikit", collidermedikits[0].transform);
            state = NodeState.SUCCESS;
            return state;

        }
        //

       

        state = NodeState.SUCCESS;
        return state;
    }
}
