using UnityEngine;

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

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        else if (distanceToPlayer <= attackRange && distanceToPlayer > jumpRange)
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
        animator.SetBool("isIdle", false);
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
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);

        switch (attackSequence)
        {
            case 0:
                FireBullet(); // Bắn thường
                attackSequence++;
                break;
            case 1:
                FireBall(); // Bắn bóng
                attackSequence++;
                break;
            case 2:
                FireBullet(); // Bắn thường
                attackSequence++;
                break;
            case 3:
                FireBullet(); // Bắn thường
                attackSequence++;
                break;
            case 4:
                FireEnergyBall(); // Bắn cầu năng lượng 3 lần
                FireEnergyBall();
                FireEnergyBall();
                attackSequence++;
                break;
            case 5:
                FireBall(); // Bắn bóng
                attackSequence = 0; // Reset chuỗi tấn công
                break;
        }
    }

    void FireBullet()
    {
        animator.SetTrigger("attack");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Bắn đạn thường
    }

    void FireBall()
    {
        animator.SetTrigger("skill_ball");
        Instantiate(ballPrefab, firePoint.position, firePoint.rotation); // Bắn bóng
    }

    void FireEnergyBall()
    {
        animator.SetTrigger("skill_energyball");
        Instantiate(energyBallPrefab, firePoint.position, firePoint.rotation); // Bắn cầu năng lượng
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
