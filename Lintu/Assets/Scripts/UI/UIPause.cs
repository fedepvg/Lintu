using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPause : MonoBehaviour
{
    public GameObject PauseOptionsPanel;
    public GameObject PauseOptionsFirstButtton;
    public GameObject SettingsPanel;
    public GameObject SettingsFirstButtton;
    public bool OnSettings = false;

    GameObject CurrentPanel;

    private void Start()
    {
        CurrentPanel = PauseOptionsPanel;
    }

    public void SwitchPanels()
    {
        EventSystem eventSystem = EventSystem.current;
        if(CurrentPanel == PauseOptionsPanel)
        {
            SetCurrentPanel(SettingsPanel, PauseOptionsPanel);
            OnSettings = true;
            eventSystem.firstSelectedGameObject = SettingsFirstButtton;
            eventSystem.SetSelectedGameObject(SettingsFirstButtton);
        }
        else
        {
            SetCurrentPanel(PauseOptionsPanel, SettingsPanel);
            OnSettings = false;
            eventSystem.firstSelectedGameObject = PauseOptionsFirstButtton;
            eventSystem.SetSelectedGameObject(PauseOptionsFirstButtton);
        }
    }

    public void ResetPanels()
    {
        if (CurrentPanel != PauseOptionsPanel)
        {
            SetCurrentPanel(PauseOptionsPanel, SettingsPanel);
            EventSystem.current.firstSelectedGameObject = PauseOptionsFirstButtton;
            EventSystem.current.SetSelectedGameObject(PauseOptionsFirstButtton);
        }
    }

    void SetCurrentPanel(GameObject toActivate, GameObject toDeactivate)
    {
        CurrentPanel = toActivate;
        toActivate.SetActive(true);
        toDeactivate.SetActive(false);
    }
}
