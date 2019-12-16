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
    public bool LastLevel;

    private void Start()
    {
        PreviousScene = LoaderManager.Instance.ActualScene;
        LoaderManager.Instance.ActualScene = ThisScene;
        GameManager.Instance.SceneData = this;
    }

    private void Update()
    {
        if(GameManager.Instance.Input.UI.Cancel.triggered)
        {
            if (ThisScene == "CreditsScene" || ThisScene == "SettingsScene" || ThisScene == "HowToPlayScene")
                LoadPreviousScene(false);
        }
    }

    public void ReloadScene(bool fakeLoad)
    {
        LoaderManager.Instance.LoadScene(ThisScene, fakeLoad);
    }

    public void LoadNextScene(bool fakeLoad)
    {
        if (NextScene == GameOverScene)
        {
            GameManager.Instance.Won = true;
            fakeLoad = false;
        }
        LoaderManager.Instance.LoadScene(NextScene, fakeLoad);
    }

    public void LoadPreviousScene(bool fakeLoad)
    {
        LoaderManager.Instance.LoadScene(PreviousScene, fakeLoad);
    }

    public void LoadMenuScene(bool fakeLoad)
    {
        GameManager.Instance.Won = false;
        LoaderManager.Instance.LoadScene(MenuScene, fakeLoad);
    }

    public void LoadGOScene(float time)
    {
        StartCoroutine(LoadGOCorutine(time));
    }

    IEnumerator LoadGOCorutine(float t)
    {
        yield return new WaitForSeconds(t);
        LoaderManager.Instance.LoadScene(GameOverScene, false);
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
