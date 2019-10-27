using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string ThisScene;
    public string NextScene;
    public string LastScene;
    public string GameOverScene;
    public string MenuScene;

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
        LoaderManager.Instance.LoadScene(LastScene, fakeLoad);
    }

    public void LoadMenuScene(bool fakeLoad)
    {
        LoaderManager.Instance.LoadScene(MenuScene, fakeLoad);
    }
    public void LoadGOScene(bool fakeLoad)
    {
        LoaderManager.Instance.LoadScene(GameOverScene, fakeLoad);
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
