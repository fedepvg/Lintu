using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPanelManagement : MonoBehaviour
{
    public GameObject FirstPanel;
    public GameObject FirstPanelFirstButtton;
    public GameObject FirstPanelReturnButtton;
    public GameObject SecondPanel;
    public GameObject SecondPanelFirstButtton;
    public GameObject SecondPanelReturnButtton;
    public bool OnSecondPanel = false;
    public bool AcceptInput;

    GameObject CurrentPanel;

    private void Start()
    {
        CurrentPanel = FirstPanel;
        EventSystem.current.SetSelectedGameObject(FirstPanelFirstButtton);
    }

    private void Update()
    {
        if (AcceptInput)
        {
            if (GameManager.Instance.Input.UI.Cancel.triggered)
            {
                if (FirstPanel.activeSelf)
                    FirstPanelReturnButtton.GetComponent<Button>().onClick.Invoke();
                else
                    SecondPanelReturnButtton.GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    public void SwitchPanels()
    {
        EventSystem eventSystem = EventSystem.current;
        if(CurrentPanel == FirstPanel)
        {
            SetCurrentPanel(SecondPanel, FirstPanel);
            OnSecondPanel = true;
            eventSystem.firstSelectedGameObject = SecondPanelFirstButtton;
            eventSystem.SetSelectedGameObject(SecondPanelFirstButtton);
        }
        else
        {
            SetCurrentPanel(FirstPanel, SecondPanel);
            OnSecondPanel = false;
            eventSystem.firstSelectedGameObject = FirstPanelFirstButtton;
            eventSystem.SetSelectedGameObject(FirstPanelFirstButtton);
        }
    }

    public void ResetPanels()
    {
        SetCurrentPanel(FirstPanel, SecondPanel);
        EventSystem.current.firstSelectedGameObject = FirstPanelFirstButtton;
        EventSystem.current.SetSelectedGameObject(FirstPanelFirstButtton);
    }

    void SetCurrentPanel(GameObject toActivate, GameObject toDeactivate)
    {
        CurrentPanel = toActivate;
        toActivate.SetActive(true);
        toDeactivate.SetActive(false);
    }
}
