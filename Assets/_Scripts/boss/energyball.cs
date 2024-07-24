using System.Collections;
using UnityEngine;

public class enegyball : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển của quả cầu năng lượng
    public float damage = 1f; // Sát thương của quả cầu năng lượng

    private Rigidbody2D rb;
    private Transform player; // Nhân vật người chơi
    private bool hasHitPlayer = false; // Kiểm tra xem quả cầu đã va chạm với người chơi chưa

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Lấy đối tượng người chơi

        if (player != null)
        {
            // Tính toán hướng tới người chơi
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed; // Đặt vận tốc của quả cầu năng lượng
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasHitPlayer = true; // Đánh dấu là đã va chạm với người chơi
            // Bạn có thể thêm logic gây sát thương cho người chơi tại đây
            Destroy(gameObject); // Hủy quả cầu năng lượng
        }
    }
}
