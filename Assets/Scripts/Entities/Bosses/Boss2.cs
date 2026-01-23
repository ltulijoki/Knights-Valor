using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Boss1
{
    public float rockDropFrequency;
    public DroppingRock rock;
    public int rockAmount;
    public float rockY;
    private bool isDropping = false;
    private DroppingRock[] rocks;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rocks = new DroppingRock[rockAmount];
        InvokeRepeating("DropRocks", rockDropFrequency, rockDropFrequency);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isDropping)
        {
            for (int i = 0; i < rockAmount; i++)
            {
                if (rocks[i] != null) return;
            }
            isDropping = false;
            animator.SetBool("DropRocks", false);
        }
        base.Update();
    }

    void DropRocks()
    {
        if (isDropping) return;
        isDropping = true;
        animator.SetBool("DropRocks", true);
        for (int i = 0; i < rockAmount; i++)
        {
            rocks[i] = Instantiate(rock, new Vector3(GetValidRockPlace(i), rockY), Quaternion.identity);
        }
    }

    float GetValidRockPlace(int i)
    {
        while (true)
        {
            float place = Random.Range(left.position.x, right.position.x);
            if (Mathf.Abs(transform.position.x - place) < 1) continue;
            bool valid = true;
            for (int j = 0; j < i; j++)
            {
                if (Mathf.Abs(rocks[j].transform.position.x - place) < 0.5f) { valid = false; break; }
            }
            if (valid) return place;
        }
    }

    protected override void Jump()
    {
        if (isDropping) return;
        base.Jump();
    }
}
