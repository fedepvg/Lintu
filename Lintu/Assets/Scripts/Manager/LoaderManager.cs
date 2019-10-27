using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviourSingleton<LoaderManager>
{
    public float LoadingProgress;
    public bool FakeLoad;
    public float TimeLoading;
    public float MinTimeToLoad = 2;
    public void LoadScene(string sceneName)
    {
        UILoadingScreen.Instance.SetVisible(true);
        if (FakeLoad)
        {
            StartCoroutine(AsynchronousLoadWithFake(sceneName));  
        }
        else
        {
            StartCoroutine(AsynchronousLoad(sceneName));    
        }
    }

    IEnumerator AsynchronousLoad(string scene)
    {
        LoadingProgress = 0;

        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            LoadingProgress = ao.progress + 0.1f;

            // Loading completed
            if (ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator AsynchronousLoadWithFake(string scene)
    {
        LoadingProgress = 0;
        TimeLoading = 0;
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            TimeLoading += Time.deltaTime;
            LoadingProgress = ao.progress + 0.1f;
            LoadingProgress = LoadingProgress * TimeLoading / MinTimeToLoad;

            // Loading completed
            if (LoadingProgress >= 1)
            {
                ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}