using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    void Update()
    {
        // Kiểm tra xem phím Space có được nhấn hay không
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Chuyển sang Scene có tên "EndGame"
            SceneManager.LoadScene("EndGame");
        }
    }
}
