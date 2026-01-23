using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingRock : Rock
{
    public float minDropDelay;
    public float maxDropDelay;
    public LayerMask floorMask;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (( floorMask & (1 << collision.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Invoke("Drop", Random.Range(minDropDelay, maxDropDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Drop()
    {
        rolling = true;
        rb.gravityScale = 2;
    }
}
