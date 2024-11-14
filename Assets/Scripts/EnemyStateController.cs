using UnityEngine;
using System.Collections.Generic;


public class EnemyStateController : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Moving,
        Attacking,
    }


    private static readonly Dictionary<EnemyState, string> _stateDescriptor = new()
    {
        { EnemyState.Idle, "isIdle"},
        { EnemyState.Moving, "isChasing"},
        { EnemyState.Attacking, "isAttacking"}
    };


    private EnemyState _state;
    private bool needsUpdate;
    private Enemy_Combat _combatScript;

    void Start()
    {
        _combatScript = GetComponent<Enemy_Combat>();
    }

    void Update()
    {
        if (!needsUpdate) return;

        if (_state is EnemyState.Attacking) _combatScript.Attack();
    }

    public EnemyStateController SetState(EnemyState state, Animator animator)
    {
        if (this._state == state)
        {
            needsUpdate = false;
            return this;
        }

        animator.SetBool(_stateDescriptor[this._state], false);

        this._state = state;

        animator.SetBool(_stateDescriptor[this._state], true);

        needsUpdate = true;
        return this;
    }


    public EnemyState GetState()
    {
        return this._state;
    }
}

