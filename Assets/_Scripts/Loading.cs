using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public GameObject LoaderUI;
    public Slider progressSlider;

    public void LoadScene(int index)
    {
        StartCoroutine(LoadScene_Coroutine(index));
    }

    public IEnumerator LoadScene_Coroutine(int index)
    {
        progressSlider.value = 0;
        LoaderUI.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressSlider.value = progress;

           
            if (asyncOperation.progress >= 0.9f && !asyncOperation.allowSceneActivation)
            {
                progressSlider.value = 1f;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
