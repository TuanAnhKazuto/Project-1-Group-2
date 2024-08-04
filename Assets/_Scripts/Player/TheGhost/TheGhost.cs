using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheGhost : MonoBehaviour
{
    #region Variables
    Animator animator;
    Rigidbody2D rb;


    private PlayerStaminaBar staminaBar;

    // Move
    [Header("Move")]
    public float moveSpeed;
    public float jumpSpeed;
    private int maxJump = 1;
    private int jumpCount = 0;
    [HideInInspector] public bool isRunning = false;
    // Check chạm đất
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    //Attack
    [Header("Attack")]
    private float holdThreshold = 0.3f;
    private bool isHolding = false;
    private float holdTime = 0f;
    [HideInInspector] public bool isAttacking = false;
    private bool vipAttackTriggered = false;

    //Dash
    [Header("Dash")]
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

    //Take item
    [Header("Item")]
    public int coin = 0;
    public TextMeshProUGUI TextCoin;
    private float oniginiValue;
    private float sakekasuValue;
    [SerializeField] private TextMeshProUGUI onigiriText;
    [SerializeField] private TextMeshProUGUI sakekasuText;
    PlayerHealth playerHealth;

    //Audio
    [Header("Sound")]
    [SerializeField] private AudioSource attackSource;
    [SerializeField] private AudioSource attackVIPSource;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource jumpSound;

    [Header("Effect")]
    public ParticleSystem healingEffect;
    public ParticleSystem recoveryEffect;
    #endregion


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        staminaBar = GetComponent<PlayerStaminaBar>();
        playerTransform = transform;
        playerHealth = GetComponent<PlayerHealth>();
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
        Healing();
        Recovery();
    }
    private void UpdateAnimator()
    {
        animator.SetBool("isMoving", Input.GetAxis("Horizontal") != 0);
        animator.SetBool("isJumping", !IsGrounded());
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("AttackVIP");
    }

    #region Move
    //Move
    private void Jump()
    {
        if (playerHealth.isDeading) return;

        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || jumpCount < maxJump))
        {
            if (staminaBar.curStamina < whenJump) return;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpSound.Play();
            jumpCount++;
            if (jumpCount == maxJump)
            {
                staminaBar.SubStaminaBar(whenJump);
            }
        }

        if (IsGrounded())
        {
            jumpCount = 0;
        }
    }

    public void Run()
    {
        if (playerHealth.isDeading) return;

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

    private bool IsGrounded()
    {
        // Raycast xuống dưới để kiểm tra va chạm với mặt đất
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }
    #endregion

    #region Attack
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

        attackSource.Play();
        animator.SetBool("isAttack", true);
        Invoke("ResetAttack", 0.2f);
        staminaBar.SubStaminaBar(staminaAttack);
    }

    private void AttackVIP()
    {
        if (staminaBar.curStamina < staminaAttackVIP) return;

        attackVIPSource.Play();
        animator.SetBool("isAttackVIP", true);
        Invoke("ResetAttack", 0.5f);
        staminaBar.SubStaminaBar(staminaAttackVIP);
    }

    private void ResetAttack()
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("isAttackVIP", false);
    }
    #endregion

    #region Dash
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
            dashSound.Play();
            moveSpeed += dashBoost;
            _dashTime = dashTime;
            isDashing = true;
            staminaBar.SubStaminaBar(whenDash);
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
    #endregion

    #region Take Items
    //Take Item
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coin++;
            TextCoin.SetText(coin.ToString());
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Onigiri")
        {
            oniginiValue++;
            onigiriText.text = oniginiValue.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Sakekasu")
        {
            sakekasuValue++;
            sakekasuText.text = sakekasuValue.ToString();
            Destroy(other.gameObject);
        }
    }
    #endregion

    #region Healing and Recovery
    //Healing and Recovery
    public void Healing()
    {
        if (oniginiValue > 0 && playerHealth.curHP < 100)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                healingEffect.Play();
                playerHealth.HealingInBar(30);
                oniginiValue--;
                onigiriText.text = oniginiValue.ToString();
            }
        }
    }

    public void Recovery()
    {
        if (sakekasuValue > 0 && staminaBar.curStamina < 100)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                recoveryEffect.Play();
                staminaBar.RecoveryInBar(25);
                sakekasuValue--;
                sakekasuText.text = sakekasuValue.ToString();
            }
        }
    }
    #endregion
}
