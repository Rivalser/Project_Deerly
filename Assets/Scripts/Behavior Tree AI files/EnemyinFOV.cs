using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node // ezt figyeljuk, hogy benne van-e a jatekos a ai lato mezojeben 
{
    private static int _enemyLayerMask = 1 << 6; // Layer 6 is the enemy layer

    private Transform _transform;
    private Animator _animator;
    
    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, EnemyBT.fovRange, _enemyLayerMask);
            
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                _animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
                return state;
            }    

                state = NodeState.FAILURE;
                return state;
        }       
         state = NodeState.SUCCESS;
         return state;
    }
}