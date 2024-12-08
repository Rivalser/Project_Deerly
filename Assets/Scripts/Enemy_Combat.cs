using UnityEngine;
using System.Collections;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public float knockbackForce;
    public float stunTime;
    public LayerMask playerLayer;
    public float cooldownTime = 3f;


    private bool _isOnCooldown = false;

    private IEnumerator CooldownCoroutine()
    {
        _isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        _isOnCooldown = false;
    }

    private IEnumerator AttackAnimationCoroutine()
    {
        GetComponent<EnemyStateController>().SetState(EnemyStateController.EnemyState.Attacking, GetComponent<Animator>());
        yield return new WaitForSeconds(1f);
        GetComponent<EnemyStateController>().SetState(EnemyStateController.EnemyState.Idle, GetComponent<Animator>());
    }

    public void Attack()
    {
        if (!_isOnCooldown)
        {
            //Adott pontból kiindúlva adott sugárban keresi a Playerréteghez tartozó objektumokat.
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

            if (hits.Length > 0)
            {
                StartCoroutine(AttackAnimationCoroutine());
                StartCoroutine(CooldownCoroutine());
                hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
                hits[0].GetComponent<PlayerMovement>().Knockback(transform, knockbackForce, stunTime);
            }
        }
    }
}
/*
public interface IStateController<TState> where TState : System.Enum
{
    void FixedUpdate();
}

public abstract class AEnemyState : IStateController<AEnemyState.State>
{
    public enum State
    {
        a,
        b,
        c
    }

    public abstract void FixedUpdate();

    private float _updateTimestamp;

}



public class MyState : AEnemyState
{
    override public void FixedUpdate()
    {
    }
}
*/
