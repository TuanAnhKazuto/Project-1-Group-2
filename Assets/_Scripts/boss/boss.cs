using UnityEngine;
using System.Collections;

public class boss : MonoBehaviour
{
    public Transform player; // Đối tượng người chơi
    public GameObject energyBallPrefab; // Prefab của quả cầu năng lượng
    public Transform energyBallSpawn; // Vị trí xuất hiện của quả cầu năng lượng
    public float speed = 5f; // Tốc độ di chuyển của boss
    public float jumpForce = 10f; // Lực nhảy của boss
    public float slideSpeed = 10f; // Tốc độ lướt của boss
    public float attackRange = 2f; // Phạm vi tấn công của boss

    private Rigidbody2D rb; // Thành phần Rigidbody2D của boss
    private Animator anim; // Thành phần Animator của boss
    private bool isGrounded = true; // Kiểm tra xem boss có đang đứng trên mặt đất không
    private bool isAttacking = false; // Kiểm tra xem boss có đang tấn công không

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D
        anim = GetComponent<Animator>(); // Lấy thành phần Animator
    }

    void Update()
    {
        if (!isAttacking)
        {
            float distance = Vector2.Distance(transform.position, player.position); // Tính khoảng cách tới người chơi
            if (distance < attackRange)
            {
                FacePlayer(); // Quay mặt về phía người chơi
                int attackType = Random.Range(0, 4); // Chọn ngẫu nhiên chiêu thức tấn công
                switch (attackType)
                {
                    case 0:
                        StartCoroutine(NormalAttack()); // Chiêu đánh thường
                        break;
                    case 1:
                        StartCoroutine(JumpStomp()); // Chiêu nhảy dậm
                        break;
                    case 2:
                        StartCoroutine(ShootEnergyBall()); // Chiêu bắn quả cầu năng lượng
                        break;
                    case 3:
                        StartCoroutine(SlideAttack()); // Chiêu lướt
                        break;
                }
            }
        }
    }

    private void FacePlayer()
    {
        // Quay mặt về phía người chơi
        Vector3 scale = transform.localScale;
        if (player.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x); // Quay mặt về bên phải
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x); // Quay mặt về bên trái
        }
        transform.localScale = scale;
    }

    private IEnumerator NormalAttack()
    {
        isAttacking = true; // Bắt đầu tấn công
        anim.SetTrigger("NormalAttack"); // Kích hoạt animation đánh thường
        yield return new WaitForSeconds(1f); // Thời gian chờ cho animation đánh thường
        isAttacking = false; // Kết thúc tấn công
    }

    private IEnumerator JumpStomp()
    {
        isAttacking = true; // Bắt đầu tấn công
        anim.SetTrigger("JumpStomp"); // Kích hoạt animation nhảy dậm
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Nhảy lên
        yield return new WaitForSeconds(1f); // Thời gian chờ cho animation nhảy dậm
        isAttacking = false; // Kết thúc tấn công
    }

    private IEnumerator ShootEnergyBall()
    {
        isAttacking = true; // Bắt đầu tấn công
        anim.SetTrigger("ShootEnergyBall"); // Kích hoạt animation bắn quả cầu năng lượng
        yield return new WaitForSeconds(0.5f); // Thời gian chờ trước khi bắn
        Instantiate(energyBallPrefab, energyBallSpawn.position, Quaternion.identity); // Tạo quả cầu năng lượng
        yield return new WaitForSeconds(0.5f); // Thời gian chờ cho animation bắn quả cầu năng lượng
        isAttacking = false; // Kết thúc tấn công
    }

    private IEnumerator SlideAttack()
    {
        isAttacking = true; // Bắt đầu tấn công
        anim.SetTrigger("SlideAttack"); // Kích hoạt animation lướt
        yield return new WaitForSeconds(1f); // Thời gian chờ cho animation lướt
        rb.velocity = Vector2.zero; // Dừng lướt
        isAttacking = false; // Kết thúc tấn công
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Kiểm tra xem boss có chạm đất không
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Kiểm tra xem boss có rời khỏi mặt đất không
        }
    }
}
