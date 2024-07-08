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
            Invoke("AttackCoroutine", 1f);
        }
    }
    public void AttackCoroutine()
    {
            animator.SetBool("isATK", false);
        isAttacking = false;

    }
    /*IEnumerator AttackCoroutine()
    {
        
        // Wait for the animation to finish
        float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(animationLength);

        isAttacking = false;
    }*/
}


