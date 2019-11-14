using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoaderManager : MonoBehaviourSingleton<LoaderManager>
{
    public float LoadingProgress;
    public float MinTimeToLoad;

    float TimeLoading;
    string LoadedScene;
    
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

    public string ActualScene
    {
        get { return LoadedScene; }
        set { LoadedScene = value; }
    }

    IEnumerator AsynchronousLoadWithFake(string scene)
    {
        LoadingProgress = 0;
        TimeLoading = 0;
        EventSystem.current.currentInputModule.enabled = false;
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            TimeLoading += Time.unscaledDeltaTime * 2;
            LoadingProgress = ao.progress + 0.1f;
            LoadingProgress = LoadingProgress * TimeLoading / MinTimeToLoad;

            // Loading completed
            if (LoadingProgress >= 1)
            {
                ao.allowSceneActivation = true;
                GameManager.Instance.EnableInput();
            }

            yield return null;
        }
    }
}