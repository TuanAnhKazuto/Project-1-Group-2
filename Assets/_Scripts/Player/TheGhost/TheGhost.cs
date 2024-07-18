using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGhost : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    // Move
    public float moveSpeed;
    public float jumpSpeed;

    // Check chạm đất
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
        UpdateAnimator();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
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
    }

    private bool IsGrounded()
    {
        // Raycast xuống dưới để kiểm tra va chạm với mặt đất
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        // Vẽ raycast để dễ dàng kiểm tra trong Unity Editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
