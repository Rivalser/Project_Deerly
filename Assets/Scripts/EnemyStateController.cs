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


    private static readonly Dictionary<EnemyState, string> StateDescriptor = new()
    {
        { EnemyState.Idle, "isIdle"},
        { EnemyState.Moving, "isChasing"},
        { EnemyState.Attacking, "isAttacking"}
    };


    private EnemyState _state;
    private bool _needsUpdate;
    private Enemy_Combat _combatScript;

    void Start()
    {
        _combatScript = GetComponent<Enemy_Combat>();
    }

    void Update()
    {
        if (!_needsUpdate) return;

        if (_state is EnemyState.Idle) _combatScript.Attack();
    }

    public EnemyStateController SetState(EnemyState state, Animator animator)
    {
        if (this._state == state)
        {
            _needsUpdate = false;
            return this;
        }

        animator.SetBool(StateDescriptor[this._state], false);

        this._state = state;

        animator.SetBool(StateDescriptor[this._state], true);

        _needsUpdate = true;
        return this;
    }


    public EnemyState GetState()
    {
        return this._state;
    }
}
