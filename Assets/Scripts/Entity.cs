using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float health;
    public float damage;
    public float knockback;
    public float speed;

    protected float currentHealth;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected Animator animator;
    protected bool dying = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    public virtual void TakeDamage(float amount, float knockbackAmount, Vector2 knockbackDirection)
    {
        currentHealth -= amount;
        transform.Translate(knockbackDirection * knockbackAmount);
        if (currentHealth <= 0)
        {
            dying = true;
            col.enabled = false;
            rb.isKinematic = true;
            animator.SetTrigger("Die");
        }
        else
            animator.SetTrigger("Hurt");
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    protected void Move(Vector2 movement)
    {
        if (dying) 
        {
            animator.SetBool("Run", false);
            return;
        }
        transform.Translate(movement * Time.deltaTime);
        animator.SetBool("Run", movement != Vector2.zero);
    }
}