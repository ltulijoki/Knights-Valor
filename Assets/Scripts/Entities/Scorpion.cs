using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scorpion : Enemy
{
    public float attackFrequency;
    public bool isAttacking { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Attack", attackFrequency, attackFrequency);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        isAttacking = true;
    }
    
    public void EndAttack()
    {
        isAttacking = false;
    }
}
