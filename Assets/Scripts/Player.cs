using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public float jumpForce;
    public LayerMask floorMask;
    public float floorMaxDistance;
    public LayerMask enemyMask;
    public float enemyMaxDistance;

    private InputActions inputActions;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputActions();
        inputActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dir = inputActions.Player.Movement.ReadValue<float>();
        Move(Vector2.right * dir * speed);

        if (inputActions.Player.Attack.WasPerformedThisFrame())
        {
            animator.SetTrigger("Attack");
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 0.7f, Vector2.right, enemyMaxDistance, enemyMask);
            if (hit)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.TakeDamage(damage, knockback, Vector2.right);
            }
        }
        if (inputActions.Player.Jump.WasPerformedThisFrame())
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, floorMaxDistance, floorMask);
            if (hit)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetTrigger("Jump");
            }
        }
    }
}
