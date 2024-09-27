using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5;
    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator anim;

    private bool isKnockedBack;

    public Player_Combat player_Combat;

    private void Update()
    {
        if(Input.GetButtonDown("Slash"))
        {
            player_Combat.Attack();
        }
    }

    // Update is called once per frame
    // FixedUpdate is called once 50x per sec
    void FixedUpdate()
    {

        if(isKnockedBack == false)
        {

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if(horizontal > 0 && transform.localScale.x < 0 ||
                horizontal < 0 && transform.localScale.x > 0 )
                {
                    Flip();
                }

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));


            rb.velocity = new Vector2(horizontal, vertical) * speed;
        }
    }

    void Flip()
    {
        facingDirection *=-1;
        transform.localScale = new Vector3 (transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }

    IEnumerator KnockbackCounter(float stunTime){
        yield return new WaitForSeconds(stunTime); //életerő respawn-hoz????
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}
