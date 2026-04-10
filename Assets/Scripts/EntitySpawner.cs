using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public Entity entity;
    public Transform player;
    public float delay;
    public int limit;
    private List<Entity> spawned = new List<Entity>();

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
        spawned.RemoveAll(e => e == null);
        if (spawned.Count >= limit) return;
        Entity e = Instantiate(entity, transform.position, Quaternion.identity);
        if (e is Slime slime) slime.player = player;
        spawned.Add(e);
    }
}
