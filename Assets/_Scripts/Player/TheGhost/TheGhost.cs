using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGhost : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    private PlayerPhysicalBar physicalBar;


    // Move
    public float moveSpeed;
    public float jumpSpeed;
    private int maxJump = 1;
    private int jumpCount = 0;
    // Check chạm đất
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    //Attack
    private float holdThreshold = 0.2f;
    private bool isHolding = false;
    private float holdTime = 0f;
    private bool isAttacking = false;
    private bool vipAttackTriggered = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicalBar = GetComponent<PlayerPhysicalBar>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            Move();
            Jump();
        }
        UpdateAnimator();
        AttackManager();
    }


    //Move
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || jumpCount < maxJump))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpCount++;
        }

        if (IsGrounded())
        {
            jumpCount = 0;
        }
    }

    private void Move()
    {
        Vector2 scale = transform.localScale;
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            transform.Translate(Vector2.right * horizontal * moveSpeed * Time.deltaTime);
            scale.x = horizontal > 0 ? 1 : -1;
        }
        transform.localScale = scale;
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isMoving", Input.GetAxis("Horizontal") != 0);
        animator.SetBool("isJumping", !IsGrounded());
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attac") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("AttackVIP");
    }

    private bool IsGrounded()
    {
        // Raycast xuống dưới để kiểm tra va chạm với mặt đất
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    /*    private void OnDrawGizmos()
        {
            // Vẽ raycast để kiểm tra trong Unity
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        }*/


    //Attack
    private void AttackManager()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isHolding = true;
            holdTime = 0f;
            vipAttackTriggered = false; // Đặt lại cờ khi phím J được nhấn xuống
        }

        if (Input.GetKey(KeyCode.J))
        {
            holdTime += Time.deltaTime;
            if (isHolding && holdTime >= holdThreshold && !vipAttackTriggered)
            {
                AttackVIP();
                vipAttackTriggered = true; // Đặt cờ để chỉ chạy AttackVIP một lần
            }
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            if (isHolding && holdTime < holdThreshold)
            {
                Attack();
            }
        }
    }


    private void Attack()
    {
        animator.SetBool("isAttack", true);
        Invoke("ResetAttack", 0.2f);
        physicalBar.UpdatePhysical(2);
    }

    private void AttackVIP()
    {
        animator.SetBool("isAttackVIP", true);
        Invoke("ResetAttack", 0.5f);
        physicalBar.UpdatePhysical(4);
    }

    private void ResetAttack()
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("isAttackVIP", false);
    }
}
