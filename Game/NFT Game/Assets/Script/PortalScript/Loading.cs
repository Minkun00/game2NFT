using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Slider progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        if (GlobalControl.Instance.loadingSceneName != null)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(GlobalControl.Instance.loadingSceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                yield return null;

                if (progressBar.value < 0.9f)
                {
                    progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f, Time.deltaTime);
                }
                else if (operation.progress >= 0.9f)
                {
                    progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime);
                }

                if ((progressBar.value >= 1f) && (operation.progress >= 0.9f))
                {
                    operation.allowSceneActivation = true;
                }
            }
        }

        else  // StartÈ­¸é¿¡¼­ ³Ñ¾î°¥ ¶§
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Town");
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                yield return null;

                if (progressBar.value < 0.9f)
                {
                    progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f, Time.deltaTime);
                }
                else if (operation.progress >= 0.9f)
                {
                    progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime);
                }

                if ((progressBar.value >= 1f) && (operation.progress >= 0.9f))
                {
                    operation.allowSceneActivation = true;
                }
            }
        }


    }
}