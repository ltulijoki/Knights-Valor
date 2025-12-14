using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scorpion : Enemy
{
    public Transform player;
    public float maxDistanceToPlayer;
    public float attackFrequency;
    public bool isAttacking { get; private set; } = false;
    private float lastAttacked = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dir = transform.position.x - player.position.x > 0 ? 1 : -1;
        if (Mathf.Abs(transform.position.x - player.position.x) < maxDistanceToPlayer) Attack();
        else Move(Vector2.left * dir * speed);
    }

    void Attack()
    {
        if (Time.time - lastAttacked < attackFrequency) return;
        animator.SetTrigger("Attack");
        isAttacking = true;
        lastAttacked = Time.time;
    }
    
    public void EndAttack()
    {
        isAttacking = false;
    }
}
