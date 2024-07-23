using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enegyball : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển của quả cầu năng lượng
    public float damage = 10f; // Sát thương của quả cầu năng lượng
    public GameObject explosionEffect; // Hiệu ứng nổ
    public float explosionRadius = 2f; // Bán kính vùng nổ
    public LayerMask damageableLayers; // Các lớp có thể gây sát thương

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // Quả cầu năng lượng di chuyển theo hướng phải
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player") || hitInfo.CompareTag("Ground"))
        {
            Explode(); // Gọi hàm nổ
        }
    }

    void Explode()
    {
        // Tạo hiệu ứng nổ
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Tìm tất cả các đối tượng trong bán kính vụ nổ
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayers);
        foreach (Collider2D collider in colliders)
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                //playerHealth.TakeDamage(damage); // Gây sát thương cho các đối tượng trong vùng nổ
            }
        }

        Destroy(gameObject); // Hủy đối tượng quả cầu năng lượng
    }

    void OnDrawGizmosSelected()
    {
        // Vẽ bán kính vùng nổ trong chế độ Scene để dễ chỉnh sửa
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
