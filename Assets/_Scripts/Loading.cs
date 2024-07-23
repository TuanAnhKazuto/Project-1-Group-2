using System.Collections;
using System.Collections.Generic;
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

        AsyncOperation asyncOperasion = SceneManager.LoadSceneAsync(index);
        asyncOperasion.allowSceneActivation = false;

        float progress = 0;

        while (!asyncOperasion.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperasion.progress, Time.deltaTime);
            progressSlider.value = progress;
            if (progress >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOperasion.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
