using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public PlayerControls GameInput;
    public SceneLoader SceneData;
    public GameObject GameOverPrefab;
    bool HasWon = false;
    bool IsInvertedY = true;
    bool UseHUD = true;

    public override void Awake()
    {
        base.Awake();
        GameInput = new PlayerControls();
        GameInput.Enable();
        BirdController.GameOverAction = Endlevel;
    }

    private void Start()
    {
        AkSoundEngine.PostEvent("Inicio_Menu", gameObject);
    }

    private void Update()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void Endlevel(bool won)
    {
        if (won)
        {
            if (SceneData.LastLevel)
            {
                HasWon = won;
                Instantiate(GameOverPrefab);
            }
            else
            {
                SceneData.LoadNextScene(true);
            }
        }
        else
        {
            HasWon = false;
            Instantiate(GameOverPrefab);
        }
    }

    public bool Won
    {
        get { return HasWon; }
        set { HasWon = value; }
    }

    public PlayerControls Input
    {
        get { return GameInput; }
    }

    public void EnableInput()
    {
        GameInput.Enable();
    }
    
    public void DisableInput()
    {
        GameInput.Disable();
    }

    public bool InvertedY
    {
        get { return IsInvertedY; }
        set { IsInvertedY = value; }
    }
    
    public bool HUD
    {
        get { return UseHUD; }
        set { UseHUD = value; }
    }
}
