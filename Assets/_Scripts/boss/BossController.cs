using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossController : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển của boss
    public float attackRange = 5f; // Khoảng cách để boss bắt đầu tấn công
    public float jumpRange = 1f; // Khoảng cách để boss nhảy
    public float retreatRange = 1f; // Khoảng cách để boss lùi lại
    public Transform player; // Vị trí của người chơi
    public GameObject bulletPrefab; // Prefab của đạn thường
    public GameObject ballPrefab; // Prefab của bóng
    public GameObject energyBallPrefab; // Prefab của cầu năng lượng
    public Transform firePoint; // Điểm bắn đạn
    public int maxHealth = 400; // Máu tối đa của boss
    public GameObject healthBarPrefab; // Prefab của thanh máu

    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private int currentHealth;
    private int attackSequence = 0;
    private bool isCooldown = false;
    private GameObject healthBarInstance;
    private Image healthBarForeground;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab, transform);
            healthBarForeground = healthBarInstance.transform.Find("HealthBarForeground").GetComponent<Image>();
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer <= attackRange && distanceToPlayer > jumpRange && !isCooldown)
        {
            FacePlayer(); // Đảm bảo boss quay mặt về phía nhân vật
            PerformAttackSequence();
        }
        else if (distanceToPlayer <= jumpRange && distanceToPlayer > retreatRange)
        {
            Jump();
        }
        else if (distanceToPlayer <= retreatRange)
        {
            RetreatFromPlayer();
        }

        if (distanceToPlayer > attackRange * 2)
        {
            animator.SetBool("isIdle", true);
        }

        // Update health bar
        if (healthBarForeground != null)
        {
            float healthPercent = (float)currentHealth / maxHealth;
            healthBarForeground.fillAmount = healthPercent;
        }
    }

    void MoveTowardsPlayer()
    {
        animator.SetBool("isWalking", true);

        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    void PerformAttackSequence()
    {
        switch (attackSequence)
        {
            case 0:
                StartCoroutine(FireBullet());
                attackSequence++;
                break;
            case 1:
                StartCoroutine(FireBall());
                attackSequence++;
                break;
            case 2:
                StartCoroutine(FireBullet());
                attackSequence++;
                break;
            case 3:
                StartCoroutine(FireBullet());
                attackSequence++;
                break;
            case 4:
                StartCoroutine(FireEnergyBall());
                StartCoroutine(FireEnergyBall());
                StartCoroutine(FireEnergyBall());
                attackSequence++;
                break;
            case 5:
                StartCoroutine(FireBall());
                attackSequence = 0;
                break;
        }
    }

    IEnumerator FireBullet()
    {
        animator.SetTrigger("attack_boss");
        InstantiateBullet(bulletPrefab);
        isCooldown = true;
        yield return new WaitForSeconds(2f);
        isCooldown = false;
    }

    IEnumerator FireBall()
    {
        animator.SetBool("isUsingSkill", true);
        InstantiateBullet(ballPrefab);
        isCooldown = true;
        yield return new WaitForSeconds(2f);
        isCooldown = false;
        animator.SetBool("isUsingSkill", false);
    }

    IEnumerator FireEnergyBall()
    {
        animator.SetTrigger("skill_energyball");
        InstantiateBullet(energyBallPrefab);
        isCooldown = true;
        yield return new WaitForSeconds(2f);
        isCooldown = false;
    }

    void InstantiateBullet(GameObject bulletPrefab)
    {
        Vector2 attackDirection = facingRight ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.localScale = transform.localScale;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.attackDirection = attackDirection;
        }
    }

    void Jump()
    {
        animator.SetTrigger("jump");
        rb.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
        StartCoroutine(LimitJumpHeight());
    }

    IEnumerator LimitJumpHeight()
    {
        float startY = transform.position.y; // Lưu vị trí Y hiện tại của boss
        float maxJumpHeight = 1f; // Giới hạn độ cao tối đa

        while (transform.position.y < startY + maxJumpHeight)
        {
            yield return null; // Chờ đến khung hình tiếp theo
        }

        // Khi boss đạt độ cao tối đa, đặt vị trí Y về giới hạn
        Vector2 newPos = new Vector2(transform.position.x, startY + maxJumpHeight);
        rb.position = newPos;
    }

    void RetreatFromPlayer()
    {
        animator.SetBool("isWalking", true);

        // Tính toán khoảng cách hiện tại và hướng lùi
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 retreatDirection = -directionToPlayer;

        // Tính toán vị trí mới của boss
        Vector2 targetPosition = (Vector2)player.position + retreatDirection * retreatRange;

        // Di chuyển boss về phía vị trí mục tiêu
        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("injured");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 2f);
    }

    void FacePlayer()
    {
        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }
}
