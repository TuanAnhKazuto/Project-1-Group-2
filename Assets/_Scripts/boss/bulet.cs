using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Tốc độ di chuyển của đạn
    public float lifespan = 5f; // Thời gian sống của đạn trước khi tự hủy
    public int damage = 10; // Sát thương của đạn

    private Rigidbody2D rb;

    public Vector2 attackDirection; // Hướng tấn công của boss

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifespan); // Hủy đạn sau một khoảng thời gian

        // Đặt vận tốc dựa trên hướng tấn công
        rb.velocity = attackDirection.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Nếu đạn va chạm với người chơi
        {
            // Thực hiện hành động khi đạn trúng người chơi
            PlayerHealth player = other.GetComponentInParent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDame(damage); // Gọi phương thức TakeDamage() của người chơi
            }

            Destroy(gameObject); // Hủy đạn sau khi va chạm
        }
        else if (other.CompareTag("Ground")) // Nếu đạn va chạm với môi trường
        {
            Destroy(gameObject); // Hủy đạn khi va chạm với môi trường
        }
    }
}
