using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Entity
{
    public GameObject[] content;

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
        
        Instantiate(content[Random.Range(0, content.Length)], transform.position, Quaternion.identity);
        base.Die();
    }
}
