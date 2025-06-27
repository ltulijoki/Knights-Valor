using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public Transform player;
    public Fire fire;
    public float fireFrequency;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", fireFrequency, fireFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        float dir = transform.position.x - player.position.x > 0 ? 1 : -1;
        Move(Vector2.left * dir * speed);
    }

    void Fire()
    {
        Instantiate(fire, transform.position, dir < 0 ? Quaternion.Euler(Vector3.forward * 180) : Quaternion.identity);
    }
}
