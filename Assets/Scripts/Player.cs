using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputActions inputActions;
    private Animator animator;

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActions.Player.Attack.WasPerformedThisFrame())
        {
            animator.SetTrigger("Attack");
        }
    }
}
