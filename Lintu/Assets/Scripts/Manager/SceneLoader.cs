using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string ThisScene;
    public string NextScene;
    public string GameOverScene;
    public string MenuScene;
    
    public string PreviousScene;

    private void Start()
    {
        PreviousScene = LoaderManager.Instance.ActualScene;
        LoaderManager.Instance.ActualScene = ThisScene;
    }

    public void ReloadScene(bool fakeLoad)
    {
        LoaderManager.Instance.LoadScene(ThisScene, fakeLoad);
    }

    public void LoadNextScene(bool fakeLoad)
    {
        LoaderManager.Instance.LoadScene(NextScene, fakeLoad);
    }

    public void LoadLastScene(bool fakeLoad)
    {
        LoaderManager.Instance.LoadScene(PreviousScene, fakeLoad);
    }

    public void LoadMenuScene(bool fakeLoad)
    {
        LoaderManager.Instance.LoadScene(MenuScene, fakeLoad);
    }

    public void LoadGOScene(bool fakeLoad)
    {
        LoaderManager.Instance.LoadScene(GameOverScene, fakeLoad);
    }

    public void LoadTargetSceneWithFake(string target)
    {
        LoaderManager.Instance.LoadScene(target, true);
    }
    
    public void LoadTargetScene(string target)
    {
        LoaderManager.Instance.LoadScene(target, false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit ();
#endif
    }
}
