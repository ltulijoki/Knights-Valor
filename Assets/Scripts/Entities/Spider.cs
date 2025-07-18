using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public Transform top;
    public Transform bottom;

    private float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(Vector2.down * direction * speed);
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        transform.rotation = Quaternion.identity;
        if (transform.position.y <= bottom.position.y || transform.position.y >= top.position.y)
        {
            direction *= -1;
        }
    }
}
