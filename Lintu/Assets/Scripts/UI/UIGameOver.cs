using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGameOver : MonoBehaviour
{
    public GameObject WinCanvas;
    public GameObject LoseCanvas;
    public GameObject WinCanvasFirstButton;
    public GameObject LoseCanvasFirstButton;

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
            bool playerWon = GameManager.Instance.Won;

            WinCanvas.SetActive(playerWon);
            LoseCanvas.SetActive(!playerWon);

            if (playerWon)
            {
                eventSystem.firstSelectedGameObject = WinCanvasFirstButton;
                eventSystem.SetSelectedGameObject(WinCanvasFirstButton);
            }
            else
            {
                eventSystem.firstSelectedGameObject = LoseCanvasFirstButton;
                eventSystem.SetSelectedGameObject(LoseCanvasFirstButton);
            }
        }
    }
}
