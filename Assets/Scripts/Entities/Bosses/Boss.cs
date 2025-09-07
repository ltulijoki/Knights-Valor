using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject[] exit;

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
        for (int i = 0; i < exit.Length; i++)
        {
            exit[i].SetActive(false);
        }
        base.Die();
    }
}
