using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIMenu : MonoBehaviour
{
    public TextMeshProUGUI VersionText;
    public GameObject SelectionSprite;

    GameObject PreviousButtonSelected;

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

        if (PreviousButtonSelected != ActualButton && ActualButton)
        {
            if (!SelectionSprite.activeSelf)
                SelectionSprite.SetActive(true);
            SelectionSprite.transform.position = ActualButton.transform.position + Vector3.right * ActualButton.GetComponent<RectTransform>().sizeDelta.x / 2;
            PreviousButtonSelected = ActualButton;
        }
        else if (!ActualButton)
            SelectionSprite.SetActive(false);
    }


}
