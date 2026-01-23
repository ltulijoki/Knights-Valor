using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool rolling = false;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity && rolling)
        {
            entity.StartDying();
        }
    }

    public void StartRolling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rolling = true;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
