using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public float time;
    public LayerMask spikesMask;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Invoke("Fall", time);
        }
        if (( spikesMask & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }
    }

    void Fall()
    {
        rb.isKinematic = false;
    }
}
