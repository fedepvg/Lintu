using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public PlayerControls GameInput;
    public SceneLoader SceneData;
    public GameObject GameOverCanvas;
    bool HasWon = false;
    bool IsInvertedY = true;
    bool UseHUD = true;
    float GameTime;
    float BestGameTime;
    bool Counting;
    bool MadeHighscore;

    private void Start()
    {
        GameInput = new PlayerControls();
        GameInput.Enable();
        BirdController.GameOverAction = Endlevel;
        BirdController.EndLevelAction = StopCountingTime;
        AkSoundEngine.PostEvent("Inicio_Menu", gameObject);
        if(PlayerPrefs.HasKey("BestTime"))
            BestGameTime = PlayerPrefs.GetFloat("BestTime");
        MadeHighscore = false;
    }

    private void Update()
    {
        Cursor.visible = false;
        if(Counting)
        {
            GameTime += Time.deltaTime;
        }
    }

    public void StartCountingTime()
    {
        Counting = true;
        GameTime = 0f;
        MadeHighscore = false;
    }

    public void StopCountingTime()
    {
        Counting = false;
        if (BestGameTime < GameTime)
        {
            BestGameTime = GameTime;
            PlayerPrefs.SetFloat("BestTime", GameTime);
            MadeHighscore = true;
        }
    }

    public void Endlevel(bool won)
    {
        if (won)
        {
            if (SceneData.LastLevel)
            {
                HasWon = won;
                GameOverCanvas.SetActive(true);
            }
            else
            {
                SceneData.LoadNextScene(true);
            }
        }
        else
        {
            HasWon = false;
            GameOverCanvas.SetActive(true);
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

    public float TimePlayingLevel
    {
        get { return GameTime; }
        set { GameTime = value; }
    }

    public bool CountingTime
    {
        get { return Counting; }
        set { Counting = value; }
    }

    public bool IsHighscore
    {
        get { return MadeHighscore; }
    }

    public float Highscore
    {
        get { return BestGameTime; }
    }
}
