using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Điều kiện để boss đi bộ
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Điều kiện để boss chạy
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Điều kiện để boss nhảy
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        
    }

    public void Attack()
    {
        animator.SetTrigger("isAttacking");
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
    }

    public void UseSkill()
    {
        animator.SetTrigger("useSkill");
    }

    public void Hit()
    {
        animator.SetTrigger("isHit");
    }
}
