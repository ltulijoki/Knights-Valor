using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float health;
    public float damage;
    public float knockback;
    public float speed;
    public bool looksLeft;
    public Fire fire;
    public Transform firePosition;

    protected float currentHealth;
    protected int dir = 1;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;
    protected Animator animator;
    protected bool dying = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    public virtual void TakeDamage(float amount, float knockbackAmount, Vector2 knockbackDirection)
    {
        currentHealth -= amount;
        transform.Translate(knockbackDirection * knockbackAmount);
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            dying = true;
            gameObject.layer = LayerMask.NameToLayer("Dying");
            Invoke("StartDying", 0.25f);
        }
    }

    public void StartDying()
    {
        animator.SetTrigger("Die");
        Invoke("Die", 1);
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
        if (movement.x > 0)
        {
            dir = 1;
            sr.flipX = looksLeft;
        }
        else if (movement.x < 0)
        {
            dir = -1;
            sr.flipX = !looksLeft;
        }
        transform.Translate(movement * Time.deltaTime);
        animator.SetBool("Run", movement != Vector2.zero);
    }

    protected void Fire()
    {
        if (dying) return;
        Instantiate(fire, firePosition.position, dir < 0 ? Quaternion.Euler(Vector3.forward * 180) : Quaternion.identity);
    }
}