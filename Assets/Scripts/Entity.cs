using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float health;
    public float damage;
    public float knockback;

    private float currentHealth;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    public void TakeDamage(float amount, float knockbackAmount, Vector2 knockbackDirection)
    {
        currentHealth -= amount;
        transform.Translate(knockbackDirection * knockbackAmount);
        if (currentHealth <= 0)
            animator.SetTrigger("Die");
        else
            animator.SetTrigger("Hurt");
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}