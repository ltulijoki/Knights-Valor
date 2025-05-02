using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Coin coin;
    public int drop;

    void OnCollisionEnter2D(Collision2D collision)
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
        Coin coin = Instantiate(this.coin, transform.position, Quaternion.identity);
        coin.value = drop;
        base.Die();
    }
}
