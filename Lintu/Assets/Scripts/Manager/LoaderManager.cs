using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviourSingleton<LoaderManager>
{
    public float LoadingProgress;
    public float MinTimeToLoad;

    float TimeLoading;
    
    public void LoadScene(string target, bool fakeLoad)
    {
        if (fakeLoad)
        {
            UILoadingScreen.Instance.SetVisible(true);
            StartCoroutine(AsynchronousLoadWithFake(target));
        }
        else
            SceneManager.LoadScene(target);
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