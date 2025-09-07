using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Item drop;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            player.TakeDamage(damage, knockback, Vector2.left);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        Instantiate(drop, transform.position, Quaternion.identity);
        base.Die();
    }
}
