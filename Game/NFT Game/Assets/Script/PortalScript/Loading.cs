using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Slider progressBar;
    public GoNextScene goNextScene;
    private PlayerMove thePlayer;

    private void Start()
    {
        thePlayer = FindAnyObjectByType<PlayerMove>();

        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(thePlayer.loadingSceneName);
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
