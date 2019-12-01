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
        PreviousButtonSelected = EventSystem.current.firstSelectedGameObject;
    }

    private void Update()
    {
        EventSystem eventSystem = EventSystem.current;

        if(eventSystem.currentSelectedGameObject!=null)
            if(eventSystem.currentSelectedGameObject != PreviousButtonSelected || GameManager.Instance.GameInput.UI.Submit.triggered)
                AkSoundEngine.PostEvent("Click_Mouse", gameObject);

        if (!eventSystem.currentSelectedGameObject && GameManager.Instance.GameInput.UI.Navigate.triggered)
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);

        GameObject ActualButton = eventSystem.currentSelectedGameObject;

        if (ActualButton)
        {
            Image buttonUnderlineImage = ActualButton.GetComponent<Image>();
            if (PreviousButtonSelected != ActualButton)
            {
                if (buttonUnderlineImage.fillAmount == 1)
                    buttonUnderlineImage.fillAmount = 0;
                HasToFill = true;
                if (PreviousButtonSelected)
                    PreviousButtonSelected.GetComponent<Image>().fillAmount = 0;
                PreviousButtonSelected = ActualButton;
            }

            if (HasToFill)
                buttonUnderlineImage.fillAmount = Mathf.Clamp01(buttonUnderlineImage.fillAmount += Time.unscaledDeltaTime * 3);
            if (buttonUnderlineImage.fillAmount == 1)
                HasToFill = false;
        }
    }
}
