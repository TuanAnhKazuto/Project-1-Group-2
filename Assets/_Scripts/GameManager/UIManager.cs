using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI loadingText;

    private void Start()
    {
        loadingPanel.SetActive(false);
    }

    public void NextLevel()
    {
        Time.timeScale = 1.0f;
        victoryPanel.SetActive(false);
        loadingPanel.SetActive(true);
        StartCoroutine(LoadNextLevel());
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1.0f;
        victoryPanel.SetActive(false);
        loadingPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        StartCoroutine(LoadMainMenu());
    }
    public void Replay()
    {
        Time.timeScale = 1.0f;
        loadingPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        StartCoroutine(LoadReplayLevel());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadNextLevel()
    {
        var value = 1f;
        loadingSlider.value = value;
        loadingText.text = value + "%";

        while (true)
        {
            value++;
            loadingSlider.value = value;
            loadingText.text = value + "%";
            yield return new WaitForSeconds(0.01f);
            if (value >= 100)
            {
                break;
            }
        }
        int curIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = curIndex + 1;
        if (nextIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 0;
        }
        SceneManager.LoadScene(nextIndex);
    }
    IEnumerator LoadMainMenu()
    {
        var value = 1f;
        loadingSlider.value = value;
        loadingText.text = value + "%";

        while (true)
        {
            value++;
            loadingSlider.value = value;
            loadingText.text = value + "%";
            yield return new WaitForSeconds(0.01f);
            if (value >= 100)
            {
                break;
            }
        }
        SceneManager.LoadScene(0);
    }
    IEnumerator LoadReplayLevel()
    {
        var value = 1f;
        loadingSlider.value = value;
        loadingText.text = value + "%";

        while (true)
        {
            value++;
            loadingSlider.value = value;
            loadingText.text = value + "%";
            yield return new WaitForSeconds(0.01f);
            if (value >= 100)
            {
                break;
            }
        }
        int curIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curIndex);
    }
}