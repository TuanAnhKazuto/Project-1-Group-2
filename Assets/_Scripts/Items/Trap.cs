using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    public float speed = 2f; // Tốc độ di chuyển của bẫy
    public float minHeight = 0f; // Vị trí thấp nhất mà bẫy sẽ đạt được
    public float maxHeight = 5f; // Vị trí cao nhất mà bẫy sẽ đạt được

    private bool movingUp = true; // Biến kiểm soát hướng di chuyển

    void Update()
    {
        if (movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (transform.position.y >= maxHeight)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= minHeight)
            {
                movingUp = true;
            }
        }
    }
}
