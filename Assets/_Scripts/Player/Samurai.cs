using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Samurai : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;
  
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpForce = 4f;
    private enum MovementState { NhanVatDungYen, NhanVatChay, NhanVatDiChuyen, NhanVatTiepDat }


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponentInChildren<CapsuleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            anim.SetBool("isJump",true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            anim.SetBool("isJump",false);
        }

        UpdateAnimationState();

    }
    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {

            state = MovementState.NhanVatChay;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.NhanVatChay;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.NhanVatDungYen;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.NhanVatDiChuyen;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.NhanVatTiepDat;
        }
        anim.SetInteger("state", (int)state);


    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isCham", true);
            Invoke("ResetCham", 0.2f);
            moveSpeed = 0.5f;
            jumpForce = 0f;
        }
    }

    private void ResetCham()
    {
        moveSpeed = 4f;
        jumpForce = 7f;
        anim.SetBool("isCham", false);
    }
}


