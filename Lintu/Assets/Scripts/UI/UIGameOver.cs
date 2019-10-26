using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGameOver : MonoBehaviour
{
    private void Update()
    {
        if (!EventSystem.current.currentSelectedGameObject && GameManager.Instance.GameInput.UI.Navigate.triggered)
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }
}
