using UnityEngine;

public class bullet_fire : MonoBehaviour
{
    public float speed = 10f; // Tốc độ di chuyển của đạn
    public float lifespan = 5f; // Thời gian sống của đạn trước khi tự hủy
    public int damage = 10; // Sát thương của đạn
    public Transform characterTransform; // Tham chiếu đến biến của nhân vật

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (characterTransform != null)
        {
            // Tính toán hướng di chuyển của đạn dựa trên hướng của nhân vật
            Vector2 direction = -characterTransform.up; // Hướng xuống dưới của nhân vật
            rb.velocity = direction * speed;
        }
        else
        {
            // Nếu không có tham chiếu đến nhân vật, đạn di chuyển xuống dưới theo hướng của nó
            rb.velocity = Vector2.down * speed;
        }

        Destroy(gameObject, lifespan); // Hủy đạn sau một khoảng thời gian
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
