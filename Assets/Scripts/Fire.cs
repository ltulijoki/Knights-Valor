using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed;
    public float damage;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity)
        {
            entity.TakeDamage(damage, 0, Vector2.left);
        }

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
