using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss
{
    public Transform left;
    public Transform right;
    public float attackDistance;
    public LayerMask floorMask;
    public float floorMaxDistance;
    public LayerMask playerMask;
    public float jumpForce;
    public float jumpFrequency;
    public float attackingSpeed;
    private int direction = 1;
    private bool hurt = false;
    private bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Jump", jumpFrequency, jumpFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        if (hurt) return;
        Move(Vector2.right * direction * (attacking ? attackingSpeed : speed));
        if ((transform.position.x <= left.position.x && direction < 0)
        || (transform.position.x >= right.position.x && direction > 0))
        {
            direction *= -1;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, attackDistance, playerMask);
        if (hit)
        {
            attacking = true;
            animator.SetBool("Attack", true);
            Invoke("StopAttacking", attackDistance / attackingSpeed);
        }
    }

    void Jump()
    {
        if (hurt || attacking) return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, floorMaxDistance, floorMask);
        if (hit)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void StopAttacking()
    {
        attacking = false;
        animator.SetBool("Attack", false);
    }

    public override void TakeDamage(float amount, float knockbackAmount, Vector2 knockbackDirection)
    {
        base.TakeDamage(amount, knockbackAmount, knockbackDirection);
    }
}
