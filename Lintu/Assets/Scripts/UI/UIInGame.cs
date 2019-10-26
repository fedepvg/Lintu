using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIInGame : MonoBehaviour
{
    #region PublicVariables
    public Slider EnergyBar;
    public Image EnergyBarFill;
    public Slider AltitudeBar;
    public BirdController Player;
    public GameObject PausePanel;
    #endregion

    private void Start()
    {
        GameManager.Instance.GameInput.Gameplay.Pause.performed += ctx => SetPauseState();
    }

    void Update()
    {

        EnergyBar.value = Player.Energy;
        if (Player.Energy < 20)
            EnergyBarFill.color = Color.red;
        else
            EnergyBarFill.color = Color.white;

        AltitudeBar.value = Player.FloorDistance;

        if (!EventSystem.current.currentSelectedGameObject && PausePanel.activeSelf && GameManager.Instance.GameInput.UI.Navigate.triggered)
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        else if (!PausePanel.activeSelf)
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void SetPauseState()
    {
        if (PausePanel)
        {
            PausePanel.SetActive(!PausePanel.activeSelf);
            if(PausePanel.activeSelf)
                EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            else
                EventSystem.current.SetSelectedGameObject(null);

            Player.enabled = !Player.enabled;
            Time.timeScale = Mathf.Abs(Time.timeScale - 1);
        }
    }
}
