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
    public LayerMask lopsidedFloorMask;
    public LayerMask oppositeLopsidedFloorMask;
    public float uphillMultiplier = 1;
    public float downhillMultiplier = 1;

    protected float currentHealth;
    protected int dir = 1;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;
    protected Animator animator;
    protected bool dying = false;
    private RaycastHit2D hit;
    private RaycastHit2D oppositeHit;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    void LateUpdate()
    {
        Vector2 forward = dir > 0 ? Vector2.right : Vector2.left;
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, lopsidedFloorMask);
        if (!hit) hit = Physics2D.Raycast(transform.position, forward, 1.5f, lopsidedFloorMask);
        oppositeHit = Physics2D.Raycast(transform.position, forward, 1.5f, oppositeLopsidedFloorMask);
        if (!oppositeHit) oppositeHit = Physics2D.Raycast(transform.position, Vector2.right, 1.5f, oppositeLopsidedFloorMask);
        if (hit) transform.rotation = Quaternion.Euler(Vector3.forward * 45);
        else if (oppositeHit) transform.rotation = Quaternion.Euler(Vector3.back * 45);
        else transform.rotation = Quaternion.identity;
    }

    public virtual void TakeDamage(float amount, float knockbackAmount, Vector2 knockbackDirection)
    {
        currentHealth -= amount;
        HealthChanged();
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

        if ((hit && dir > 0) || (oppositeHit && dir < 0)) movement *= uphillMultiplier;
        if ((hit && dir < 0) || (oppositeHit && dir > 0)) movement /= downhillMultiplier;

        transform.Translate(movement * Time.deltaTime);
        animator.SetBool("Run", movement != Vector2.zero);
    }

    protected void Fire()
    {
        if (dying) return;
        Instantiate(fire, firePosition.position, dir < 0 ? Quaternion.Euler(Vector3.forward * 180) : Quaternion.identity);
    }

    protected virtual void HealthChanged() {}
}