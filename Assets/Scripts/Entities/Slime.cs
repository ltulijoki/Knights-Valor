using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dir = transform.position.x - player.position.x > 0 ? 1 : -1;
        Move(Vector2.left * dir * speed);
    }
}
