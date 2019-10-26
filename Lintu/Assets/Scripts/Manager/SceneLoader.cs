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

    public void ReloadScene()
    {
        SceneManager.LoadScene(ThisScene);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(MenuScene);
    }
    public void LoadGOScene()
    {
        SceneManager.LoadScene(GameOverScene);
    }

    public void LoadTargetScene(string target)
    {
        SceneManager.LoadScene(target);
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
