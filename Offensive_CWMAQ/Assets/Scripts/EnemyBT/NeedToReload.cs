using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

public class NeedToReload : Node
{
    private Transform _selfTransform;

    private int _layerAmmo = 1 << 9;
    public NeedToReload(Transform selftransform)
    {
        _selfTransform = selftransform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("Ammo");
        // check evry collider around and get those with the layer Ammo active//
        Collider[] colliderammo = Physics.OverlapSphere(_selfTransform.position, 80f, _layerAmmo);
        //

        // if the collider length is null then there is no ammo around so go to failure //
        if (colliderammo.Length == 0)
        {
            state = NodeState.FAILURE;
            return state;
        }
        //

        // if there is more than one ammo around then organize them by the distance between them and the ai //
        if (colliderammo.Length > 1)
        {
            colliderammo.OrderBy(hit => Vector3.Distance(hit.transform.position, _selfTransform.position));
        }
        //

        // if the data is null then go to failure //
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        //

        // if the Ai need ammo set the closest ammo in the data and go to success//
        if (_selfTransform.GetComponent<AmmoManager>().Ammo < 100f)
        {
            parent.SetData("Ammo", colliderammo[0].transform);
            state = NodeState.SUCCESS;
            return state;
        }
        //

       
        state = NodeState.SUCCESS;
        return state;
    }

}
