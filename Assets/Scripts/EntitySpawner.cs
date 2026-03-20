using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public Entity entity;
    public Transform player;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", delay, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Entity e = Instantiate(entity, transform.position, Quaternion.identity);
        if (e is Slime slime) slime.player = player;
    }
}
