using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public Text progressText;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        // Show loading screen
        loadingScreen.SetActive(true);

        // Start loading the scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // While the scene is loading
        while (!operation.isDone)
        {
            // Calculate the progress (0 to 1)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Update progress bar and text
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F0") + "%";

            yield return null;
        }
    }
}
