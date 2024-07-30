using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheGhost : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    private PlayerStaminaBar staminaBar;

    // Move
    public float moveSpeed;
    public float jumpSpeed;
    private int maxJump = 1;
    private int jumpCount = 0;
    [HideInInspector] public bool isRunning = false;
    // Check chạm đất
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    //Attack
    private float holdThreshold = 0.3f;
    private bool isHolding = false;
    private float holdTime = 0f;
    [HideInInspector] public bool isAttacking = false;
    private bool vipAttackTriggered = false;

    //Dash
    [SerializeField] private float dashBoost;
    [SerializeField] private float dashTime;
    private float _dashTime;
    [HideInInspector] public bool isDashing = false;
    public GameObject ghostEffect;
    private float ghostDelaySeconds = 0.04f;
    private Coroutine dashEffectCoroutine;
    private Transform playerTransform;
    [SerializeField] private float collisionOffset = 0.1f;
    [SerializeField] private float raycastDistance = 5f; // Khoảng cách của Raycast

    //Sub stamina count when do something
    private float staminaAttack = 2f;
    private float staminaAttackVIP = 5f;
    private float whenJump = 1f;
    private float whenDash = 6f;

    //Take Coin
    public int coin = 0;
    public TextMeshProUGUI TextCoin;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        staminaBar = GetComponent<PlayerStaminaBar>();
        playerTransform = transform;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            Run();
            Jump();
        }
        Dash();
        UpdateAnimator();
        AttackManager();
    }

    //Move
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || jumpCount < maxJump))
        {
            if (staminaBar.curStamina < whenJump) return;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpCount++;
            if (jumpCount == maxJump)
            {
                staminaBar.UpdateStaminaBar(whenJump);
            }
        }

        if (IsGrounded())
        {
            jumpCount = 0;
        }
    }

    public void Run()
    {
        Vector2 scale = transform.localScale;
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            transform.Translate(Vector2.right * horizontal * moveSpeed * Time.deltaTime);
            scale.x = horizontal > 0 ? 1 : -1;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        transform.localScale = scale;
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isMoving", Input.GetAxis("Horizontal") != 0);
        animator.SetBool("isJumping", !IsGrounded());
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("AttackVIP");
    }

    private bool IsGrounded()
    {
        // Raycast xuống dưới để kiểm tra va chạm với mặt đất
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    //Attack
    private void AttackManager()
    {
        if (isAttacking) return;

        if (Input.GetKeyDown(KeyCode.J))
        {
            isHolding = true;
            holdTime = 0f;
            vipAttackTriggered = false;
        }

        if (Input.GetKey(KeyCode.J))
        {
            holdTime += Time.deltaTime;
            if (isHolding && holdTime >= holdThreshold && !vipAttackTriggered)
            {
                AttackVIP();
                vipAttackTriggered = true;
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
        if (staminaBar.curStamina < staminaAttack) return;

        animator.SetBool("isAttack", true);
        Invoke("ResetAttack", 0.2f);
        staminaBar.UpdateStaminaBar(staminaAttack);
    }

    private void AttackVIP()
    {
        if (staminaBar.curStamina < staminaAttackVIP) return;

        animator.SetBool("isAttackVIP", true);
        Invoke("ResetAttack", 0.5f);
        staminaBar.UpdateStaminaBar(staminaAttackVIP);
    }

    private void ResetAttack()
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("isAttackVIP", false);
    }

    //Dash
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.L) && _dashTime <= 0 && isDashing == false && staminaBar.curStamina > whenDash)
        {
            Vector2 dashDirection = new Vector2(transform.localScale.x, 0).normalized;

            // Sử dụng Raycast để kiểm tra va chạm với groundLayer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, raycastDistance, groundLayer);

            float dashDistance = dashBoost * dashTime;

            if (hit.collider != null)
            {
                // Nếu va chạm xảy ra, chỉ di chuyển đến điểm va chạm trừ đi một khoảng cách an toàn
                dashDistance = hit.distance - collisionOffset;
            }

            // Di chuyển nhân vật với khoảng cách dash đã tính toán
            StartCoroutine(DashMovement(dashDirection, dashDistance));
            moveSpeed += dashBoost;
            _dashTime = dashTime;
            isDashing = true;
            staminaBar.UpdateStaminaBar(whenDash);
            StartDashEffect();
        }

        if (_dashTime > 0)
        {
            _dashTime -= Time.deltaTime;
        }
        else if (isDashing)
        {
            moveSpeed -= dashBoost;
            isDashing = false;
            StopDashEffect();
        }

        animator.SetBool("isDashing", isDashing);
    }

    private IEnumerator DashMovement(Vector2 direction, float distance)
    {
        float moved = 0f;
        while (moved < distance && isDashing)
        {
            float moveStep = moveSpeed * Time.deltaTime;
            transform.Translate(direction * moveStep);
            moved += moveStep;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (isDashing)
        {
            Gizmos.color = Color.red;
            Vector2 dashDirection = new Vector2(transform.localScale.x, 0).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, raycastDistance, groundLayer);

            float dashDistance = dashBoost * dashTime;

            if (hit.collider != null)
            {
                dashDistance = hit.distance - collisionOffset;
            }

            Gizmos.DrawLine(transform.position, (Vector2)transform.position + dashDirection * dashDistance);
        }
    }

    private void StartDashEffect()
    {
        if (dashEffectCoroutine != null) StopCoroutine(dashEffectCoroutine);
        dashEffectCoroutine = StartCoroutine(DashEffectCoroutine());
    }

    private void StopDashEffect()
    {
        if (dashEffectCoroutine != null) StopCoroutine(dashEffectCoroutine);
    }

    private IEnumerator DashEffectCoroutine()
    {
        while (true)
        {
            GameObject ghost = Instantiate(ghostEffect, transform.position, Quaternion.identity);
            ghost.transform.localScale = playerTransform.localScale;

            Destroy(ghost, 0.5f);
            yield return new WaitForSeconds(ghostDelaySeconds);
        }
    }

    //Coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coin++;
            TextCoin.SetText(coin.ToString());
            Destroy(collision.gameObject);
        }
    }
}
