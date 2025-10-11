using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class Player : Entity
{
    public float jumpForce;
    public LayerMask floorMask;
    public float floorMaxDistance;
    public LayerMask enemyMask;
    public float enemyMaxDistance;
    public LayerMask lopsidedFloorMask;
    public LayerMask oppositeLopsidedFloorMask;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shotsText;
    public GameObject stats;
    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverCoinsText;

    private InputActions inputActions;
    private PlayerStats pStats;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputActions();
        inputActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        pStats = PlayerStats.Instance;
        currentHealth = pStats.health;
        healthText.text = health.ToString();
        coinsText.text = pStats.coins.ToString();
        shotsText.text = pStats.shots.ToString();
        if (pStats.health <= 0) currentHealth = health;
        HealthChanged();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        if (item)
        {
            item.Pick(this);
            Destroy(item.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dir = inputActions.Player.Movement.ReadValue<float>();
        Move(Vector2.right * dir * speed);

        if (inputActions.Player.Attack.WasPerformedThisFrame() && !dying)
        {
            animator.SetTrigger("Attack");
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 0.7f, Vector2.right * this.dir, enemyMaxDistance, enemyMask);
            if (!hit)
                hit = Physics2D.Raycast(transform.position + Vector3.up * 0.1f, Vector2.right * this.dir, enemyMaxDistance, enemyMask);
            if (hit)
            {
                Entity enemy = hit.collider.GetComponent<Entity>();
                if (enemy is Scorpion scorpion && scorpion.isAttacking)
                    TakeDamage(scorpion.damage, scorpion.knockback, hit.transform.position.x - transform.position.x > 0 ? Vector2.left : Vector2.right);
                else
                    enemy.TakeDamage(damage, knockback, hit.transform.position.x - transform.position.x > 0 ? Vector2.right : Vector2.left);
            }
        }
        if (inputActions.Player.Jump.WasPerformedThisFrame() && !dying)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, floorMaxDistance, floorMask);
            if (hit)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetTrigger("Jump");
            }
        }
        if (inputActions.Player.Fire.WasPerformedThisFrame() && pStats.shots > 0 && !dying)
        {
            Fire();
            pStats.shots--;
            shotsText.text = pStats.shots.ToString();
        }
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, lopsidedFloorMask);
            RaycastHit2D oppositeHit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, oppositeLopsidedFloorMask);
            if (hit) transform.rotation = Quaternion.Euler(Vector3.forward * 45);
            else if (oppositeHit) transform.rotation = Quaternion.Euler(Vector3.back * 45);
            else transform.rotation = Quaternion.identity;
        }
    }

    public override void TakeDamage(float amount, float knockbackAmount, Vector2 knockbackDirection)
    {
        base.TakeDamage(amount, knockbackAmount, knockbackDirection);
        healthText.text = currentHealth.ToString();
    }

    public override void Die()
    {
        stats.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverCoinsText.text = pStats.coins.ToString();
        base.Die();
    }

    public void AddCoins(int amount)
    {
        pStats.coins += amount;
        coinsText.text = pStats.coins.ToString();
    }

    public void Heal()
    {
        currentHealth = health;
        HealthChanged();
        healthText.text = currentHealth.ToString();
    }

    public void AddShots(int amount)
    {
        pStats.shots += amount;
        shotsText.text = pStats.shots.ToString();
    }

    protected override void HealthChanged()
    {
        pStats.health = currentHealth;
    }
}
