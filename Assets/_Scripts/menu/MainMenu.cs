using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene 
        SceneManager.LoadScene("Lv1");
    }

    public void OpenOptions()
    {
        // Open options
        Debug.Log("Options");
    }

    public void ExitGame()
    {
        // Exit the game
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
