using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIMenu : MonoBehaviour
{
    public TextMeshProUGUI VersionText;

    GameObject PreviousButtonSelected;
    bool HasToFill = false;

    private void Start()
    {
        if(VersionText)
            VersionText.SetText("v" + Application.version);
    }

    private void Update()
    {
        if (!EventSystem.current.currentSelectedGameObject && GameManager.Instance.GameInput.UI.Navigate.triggered)
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);

        GameObject ActualButton = EventSystem.current.currentSelectedGameObject;

        if (ActualButton)
        {
            if (PreviousButtonSelected != ActualButton)
            {
                if (ActualButton.GetComponent<Image>().fillAmount == 1)
                    ActualButton.GetComponent<Image>().fillAmount = 0;
                HasToFill = true;
                if (PreviousButtonSelected)
                    PreviousButtonSelected.GetComponent<Image>().fillAmount = 0;
                PreviousButtonSelected = ActualButton;
            }

            if (HasToFill)
                ActualButton.GetComponent<Image>().fillAmount = Mathf.Clamp01(ActualButton.GetComponent<Image>().fillAmount += Time.unscaledDeltaTime * 3);
            if (ActualButton.GetComponent<Image>().fillAmount == 1)
                HasToFill = false;
        }
    }
}
