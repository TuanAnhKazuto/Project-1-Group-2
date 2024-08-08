using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextButtonController : MonoBehaviour
{
    [SerializeField]
    private Button nextButton; // Gán nút trong Inspector

    void Start()
    {
        // Đảm bảo rằng nút đã được gán trong Inspector
        if (nextButton != null)
        {
            // Gán hàm OnNextButtonClick() cho sự kiện bấm nút
            nextButton.onClick.AddListener(OnNextButtonClick);
        }
        else
        {
            Debug.LogError("Button chưa được gán trong Inspector.");
        }
    }

    // Phương thức được gọi khi nhấn nút
    private void OnNextButtonClick()
    {
        // Chuyển tới Scene tên là "MainMenuScene"
        SceneManager.LoadScene("MainMenuScene");
    }
}
