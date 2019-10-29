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
        if (GameManager.Instance.Won)
        {
            WinCanvas.SetActive(true);
            LoseCanvas.SetActive(false);
            GameManager.Instance.Won = false;
            EventSystem.current.firstSelectedGameObject = WinCanvasFirstButton;
            EventSystem.current.SetSelectedGameObject(WinCanvasFirstButton);
        }
        else
        {
            LoseCanvas.SetActive(true);
            WinCanvas.SetActive(false);
            EventSystem.current.firstSelectedGameObject = LoseCanvasFirstButton;
            EventSystem.current.SetSelectedGameObject(LoseCanvasFirstButton);
        }
    }
}
