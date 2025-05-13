using System.Collections;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public float knockbackForce;
    public float stunTime;
    public LayerMask playerLayer;
    public float cooldownTime = 3f;

    public LayerMask targetUnreachableLayer;
    public Transform targetTransform;


    private bool _isOnCooldown;

    private IEnumerator CooldownCoroutine ()
    {
        _isOnCooldown = true;
        yield return new WaitForSeconds (cooldownTime);
        _isOnCooldown = false;
    }

    private IEnumerator AttackAnimationCoroutine ()
    {
        //GetComponent<EnemyStateController>().SetState(EnemyStateController.EnemyState.Attacking, GetComponent<Animator>());
        //yield return new WaitForSeconds(1f);
        //GetComponent<EnemyStateController>().SetState(EnemyStateController.EnemyState.Idle, GetComponent<Animator>());
        yield return new WaitForSeconds (stunTime);
    }

    public void Attack ()
    {
        if (_isOnCooldown) return;

        if (Physics2D.OverlapCircle (targetTransform.position, 1 / 2, targetUnreachableLayer))
        {
            Debug.Log ("Character is not Attackable!");
            return;
        }

        // Character is on the road
        Debug.Log ("Character is Attackable!");


        //Adott pontból kiindúlva adott sugárban keresi a Playerréteghez tartozó objektumokat.
        var hits = Physics2D.OverlapCircleAll (attackPoint.position, weaponRange, playerLayer);

        if (hits.Length > 0)
        {
            StartCoroutine (AttackAnimationCoroutine ());
            StartCoroutine (CooldownCoroutine ());
            hits[0].GetComponent<PlayerHealth> ().ChangeHealth (-damage);
            hits[0].GetComponent<PlayerMovement> ().Knockback (transform, knockbackForce, stunTime);
        }
    }
}