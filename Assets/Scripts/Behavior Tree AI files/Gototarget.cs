using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    _transform = transform;


 public override NodeState Evaluate()
 { 
    Transform target (Transform)GetData("target");

    if (Vector3.Distamce(_transform.position, target.position) >0.01f)
    {
        _transform.position = Vector3.MoveTowards(
            _transform.position, target.position, EnemyBT.speed * Time.delata);
        _transform.LookAt(target.position);
    }

    state = Node
    return state;
  }
}
