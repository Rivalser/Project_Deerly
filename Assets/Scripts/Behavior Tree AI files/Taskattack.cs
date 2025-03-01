using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private static int _enemyLayerMask 1 << 6;

    private Transform _tranfsorm;
    private Animator _animator;

    public CheckEnemyInAttackRange(Transform transform)
    {
        _tranfsorm = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object = GetDate("target");
        if (object == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        _tranfsorm target = (Transform)t;
        if (Vector3.Distance(_tranfsorm.position, target.position) <= GuardBT.attackRange)
        {
            _animator-SetBool("Attacking", true);
            _animator.SetBool("Walking", false);
            state = NodeState.SUCCES;
            return state;
        }

        state = NodeSTate.FAILURE;
        return state;
    }
}