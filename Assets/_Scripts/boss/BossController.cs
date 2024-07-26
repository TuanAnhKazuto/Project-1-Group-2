using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển của boss
    public float attackRange = 5f; // Khoảng cách để boss bắt đầu tấn công
    public float jumpRange = 1f; // Khoảng cách để boss nhảy
    public Transform player; // Vị trí của người chơi
    public GameObject bulletPrefab; // Prefab của đạn thường
    public GameObject ballPrefab; // Prefab của bóng
    public GameObject energyBallPrefab; // Prefab của cầu năng lượng
    public Transform firePoint; // Điểm bắn đạn
    public int maxHealth = 100; // Máu tối đa của boss

    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private int currentHealth;
    private int attackSequence = 0;
    private bool isCooldown = false; // Biến kiểm tra thời gian hồi chiêu

    Transform bossTrans;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bossTrans = GetComponent<Transform>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer(); // Di chuyển về phía người chơi
        }
        else if (distanceToPlayer <= attackRange && distanceToPlayer > jumpRange && !isCooldown)
        {
            PerformAttackSequence(); // Thực hiện chuỗi tấn công
        }
        else if (distanceToPlayer <= jumpRange)
        {
            Jump(); // Nhảy khi người chơi gần
        }

        if (distanceToPlayer > attackRange * 2)
        {
            animator.SetBool("isIdle", true); // Đứng im nếu người chơi quá xa
        }
    }

    void MoveTowardsPlayer()
    {
        
        animator.SetBool("isWalking", true);

        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip(); // Đổi hướng khi cần
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip(); // Đổi hướng khi cần
        }

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos); // Di chuyển boss về phía người chơi
    }

    void PerformAttackSequence()
    {

        switch (attackSequence)
        {
            case 0:
                StartCoroutine(FireBullet()); // Bắn thường
                attackSequence++;
                break;
            case 1:
                StartCoroutine(FireBall()); // Bắn bóng
                attackSequence++;
                break;
            case 2:
                StartCoroutine(FireBullet()); // Bắn thường
                attackSequence++;
                break;
            case 3:
                StartCoroutine(FireBullet()); // Bắn thường
                attackSequence++;
                break;
            case 4:
                StartCoroutine(FireEnergyBall()); // Bắn cầu năng lượng 3 lần
                StartCoroutine(FireEnergyBall());
                StartCoroutine(FireEnergyBall());
                attackSequence++;
                break;
            case 5:
                StartCoroutine(FireBall()); // Bắn bóng
                attackSequence = 0; // Reset chuỗi tấn công
                break;
        }
    }

    IEnumerator FireBullet()
    {
        animator.SetTrigger("attack_boss");
        InstantiateBullet(bulletPrefab); // Bắn đạn thường
        isCooldown = true;
        yield return new WaitForSeconds(2f); // Thời gian hồi chiêu 2 giây
        isCooldown = false;
    }

    IEnumerator FireBall(bool isAttacking = false)
    {
        if (!isAttacking)
        {
            animator.SetBool("isUsingSkill", true);
            InstantiateBullet(ballPrefab); // Bắn bóng
            isCooldown = true;
            isAttacking = true;
        }
        else
        {
            animator.SetBool("isUsingSkill", false);
        }
        yield return new WaitForSeconds(2f); // Thời gian hồi chiêu 2 giây
        isCooldown = false;
    }

    IEnumerator FireEnergyBall()
    {
        animator.SetTrigger("skill_energyball");
        InstantiateBullet(energyBallPrefab); // Bắn cầu năng lượng
        isCooldown = true;
        yield return new WaitForSeconds(2f); // Thời gian hồi chiêu 2 giây
        isCooldown = false;
    }

    void InstantiateBullet(GameObject bulletPrefab)
    {
        Vector2 attackDirection = facingRight ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.localScale = bossTrans.localScale;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.attackDirection = attackDirection;
        }
    }

    void Jump()
    {
        animator.SetTrigger("jump");
        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); // Nhảy
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale; // Đổi hướng
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("injured"); // Animation bị thương

        if (currentHealth <= 0)
        {
            Die(); // Chết nếu hết máu
        }
    }

    void Die()
    {
        animator.SetTrigger("die"); // Animation chết
        GetComponent<Collider2D>().enabled = false; // Vô hiệu hóa collider để không bị tương tác nữa
        this.enabled = false; // Vô hiệu hóa script
        Destroy(gameObject, 2f); // Hủy đối tượng sau 2 giây
    }
}
