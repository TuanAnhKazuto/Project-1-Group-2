using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Để sử dụng Button

public class ButtonController : MonoBehaviour
{
    void Start()
    {
        // Tìm tất cả các nút bấm trong scene
        Button[] buttons = GameObject.FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            // Kiểm tra xem tag của nút bấm có phải là "PLAY" không
            if (button.CompareTag("PLAY"))
            {
                // Gán sự kiện OnClick cho nút bấm
                button.onClick.AddListener(OnPlayButtonClicked);
            }
        }
    }

    // Phương thức để chuyển cảnh
    void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("phim"); // Đảm bảo rằng tên scene là chính xác
    }
}
