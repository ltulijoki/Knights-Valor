using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity)
        {
            entity.StartDying();
        }
    }

    public void StartRolling()
    {
        rb.gravityScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
