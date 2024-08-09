using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng va chạm có tag là "phim2" không
        if (other.CompareTag("phim2"))
        {
            // Chuyển sang cảnh có tên là "phim2"
            SceneManager.LoadScene("phim2");
        }
    }
}
