using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIGameOverEndless : MonoBehaviour
{
    public GameObject FirstButton;
    public TextMeshProUGUI CurrentTimeText;
    public GameObject CurrentTimeGO;
    public TextMeshProUGUI BestTimeText;
    public GameObject BestTimeGO;
    public TextMeshProUGUI MadeBestTimeText;
    public GameObject MadeBestTime;

    private void Start()
    {
        GameManager.Instance.GameOverCanvas = gameObject;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventSystem eventSystem = EventSystem.current;
        if (GameManager.Instance != null)
        {
            eventSystem.firstSelectedGameObject = FirstButton;
            eventSystem.SetSelectedGameObject(FirstButton);
        }

        float GameTime = GameManager.Instance.TimePlayingLevel;
        int gameTimeMinutes = TimeToMinutes(GameTime);
        int gameTimeSeconds = TimeToSeconds(GameTime);
        float BestTime = GameManager.Instance.Highscore;
        int bestTimeMinutes = TimeToMinutes(BestTime);
        int bestTimeSeconds = TimeToSeconds(BestTime);

        if (GameManager.Instance.IsHighscore)
        {
            MadeBestTime.SetActive(true);
            CurrentTimeGO.SetActive(false);
            BestTimeGO.SetActive(false);
            MadeBestTimeText.text = string.Format("{0:0}:{1:00}", gameTimeMinutes, gameTimeSeconds);
        }
        else
        {
            CurrentTimeText.text = string.Format("{0:0}:{1:00}", gameTimeMinutes, gameTimeSeconds);
            BestTimeText.text = string.Format("{0:0}:{1:00}", bestTimeMinutes, bestTimeSeconds);
        }
    }

    int TimeToSeconds(float t)
    {
        return (int)(t % 60);
    }

    int TimeToMinutes(float t)
    {
        return (int)(t / 60);
    }
}
