using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            isAttacking = true;
            // Trigger animation
            animator.SetBool("isATK", true);
            //StartCoroutine(AttackCoroutine());
            Invoke("AttackCoroutine", 0.5f);
        }
       
        if (Input.GetKeyDown(KeyCode.K) && !isAttacking)
        {
            isAttacking = true;
            // Trigger animation
            animator.SetBool("isATK2", true);
            //StartCoroutine(AttackCoroutine());
            Invoke("AttackCoroutine", 0.55f);
        }
    }

    public void AttackCoroutine()
    {
        animator.SetBool("isATK", false);
        animator.SetBool("isATK2", false);
        isAttacking = false;
    }
  
}


