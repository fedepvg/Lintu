using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIMenu : MonoBehaviour
{
    public TextMeshProUGUI VersionText;
    public Sprite UnderlineImage;

    GameObject PreviousButtonSelected;
    bool HasToFill = false;
    GameObject[] SelectionVertex;

    private void Start()
    {
        if(VersionText)
            VersionText.SetText("v" + Application.version);
        PreviousButtonSelected = EventSystem.current.firstSelectedGameObject;
        SelectionVertex = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            SelectionVertex[i] = new GameObject();
            SelectionVertex[i].AddComponent<Image>();
            SelectionVertex[i].AddComponent<RectTransform>();
            SelectionVertex[i].GetComponent<Image>().sprite = UnderlineImage;
            SelectionVertex[i].transform.SetParent(transform);
        }
    }

    private void Update()
    {
        EventSystem eventSystem = EventSystem.current;

        if (eventSystem.currentSelectedGameObject != null)
        {
            if (eventSystem.currentSelectedGameObject != PreviousButtonSelected)
                AkSoundEngine.PostEvent("Cursor", gameObject);
            else if(GameManager.Instance.GameInput.UI.Submit.triggered)
                AkSoundEngine.PostEvent("Cursor_Seleccion", gameObject);
        }

        if (!eventSystem.currentSelectedGameObject && GameManager.Instance.GameInput.UI.Navigate.triggered)
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);

        GameObject ActualButton = eventSystem.currentSelectedGameObject;

        if (ActualButton && ActualButton.GetComponent<Button>())
        {
            Image buttonUnderlineImage = ActualButton.GetComponent<Image>();
            if (PreviousButtonSelected != ActualButton)
            {
                if (buttonUnderlineImage.fillAmount == 1)
                    buttonUnderlineImage.fillAmount = 0;
                HasToFill = true;

                if (PreviousButtonSelected && PreviousButtonSelected.GetComponent<Button>())
                    PreviousButtonSelected.GetComponent<Image>().fillAmount = 0;
                PreviousButtonSelected = ActualButton;
            }

            if (HasToFill)
                buttonUnderlineImage.fillAmount = Mathf.Clamp01(buttonUnderlineImage.fillAmount += Time.unscaledDeltaTime * 3);
            if (buttonUnderlineImage.fillAmount == 1)
                HasToFill = false;

            //SelectionVertex[0].transform.position = ActualButton.GetComponent<Image>().sprite.bounds.min;
            //SelectionVertex[1].transform.position = new Vector2(ActualButton.GetComponent<Image>().sprite.bounds.max.x, ActualButton.GetComponent<Image>().sprite.bounds.min.y);
            //SelectionVertex[2].transform.position = new Vector2(ActualButton.GetComponent<Image>().sprite.bounds.min.x, ActualButton.GetComponent<Image>().sprite.bounds.max.y);
            //SelectionVertex[3].transform.position = ActualButton.GetComponent<Image>().sprite.bounds.max;

            for (int i = 0; i < 4; i++)
            {
                SelectionVertex[i].transform.SetParent(ActualButton.transform);
            }

            SelectionVertex[0].transform.localPosition = new Vector2(ActualButton.GetComponent<RectTransform>().rect.xMin, ActualButton.GetComponent<RectTransform>().rect.yMin);

            SelectionVertex[1].transform.localPosition = new Vector2(ActualButton.GetComponent<RectTransform>().rect.xMax, ActualButton.GetComponent<RectTransform>().rect.yMin);

            SelectionVertex[2].transform.localPosition = new Vector2(ActualButton.GetComponent<RectTransform>().rect.xMin, ActualButton.GetComponent<RectTransform>().rect.yMax);

            SelectionVertex[3].transform.localPosition = new Vector2(ActualButton.GetComponent<RectTransform>().rect.xMax, ActualButton.GetComponent<RectTransform>().rect.yMax);
        }
        else if (ActualButton != PreviousButtonSelected)
            PreviousButtonSelected = ActualButton;
    }
}
