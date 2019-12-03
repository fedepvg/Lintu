using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIMenu : MonoBehaviour
{
    public TextMeshProUGUI VersionText;
    public Sprite []UnderlineImage;

    GameObject PreviousButtonSelected;
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
            SelectionVertex[i].GetComponent<Image>().sprite = UnderlineImage[i];
            SelectionVertex[i].transform.SetParent(transform);
            SelectionVertex[i].name="Corner" + i;
            SelectionVertex[i].transform.localScale =new Vector3(0.5f, 0.5f, 0.5f);
        }
        PlaceVertexes(PreviousButtonSelected);
    }

    private void Update()
    {
        EventSystem eventSystem = EventSystem.current;

        if (eventSystem.currentSelectedGameObject != null)
        {
            if (eventSystem.currentSelectedGameObject != PreviousButtonSelected)
                AkSoundEngine.PostEvent("Cursor", gameObject);
            else if (GameManager.Instance.GameInput.UI.Submit.triggered)
                AkSoundEngine.PostEvent("Cursor_Seleccion", gameObject);
        }

        if (!eventSystem.currentSelectedGameObject && GameManager.Instance.GameInput.UI.Navigate.triggered)
        {
            eventSystem.SetSelectedGameObject(PreviousButtonSelected);
            PreviousButtonSelected = null;
            ActivateCornerImages();
        }

        GameObject ActualButton = eventSystem.currentSelectedGameObject;

        if(ActualButton != null && PreviousButtonSelected != ActualButton)
        {
            PreviousButtonSelected = ActualButton;
            if (ActualButton.GetComponent<Button>())
            {
                PlaceVertexes(ActualButton);
                ActivateCornerImages();
            }
            else
            {
                DeactivateCornerImages();
            }
        }
        else if(ActualButton == null)
        {
            DeactivateCornerImages();
        }
    }

    void PlaceVertexes(GameObject destObj)
    {
        for (int i = 0; i < 4; i++)
        {
            SelectionVertex[i].transform.SetParent(destObj.transform);
            SelectionVertex[i].GetComponent<Image>().color = destObj.GetComponent<Image>().color;
        }

        SelectionVertex[0].transform.localPosition = new Vector2(destObj.GetComponent<RectTransform>().rect.xMin, destObj.GetComponent<RectTransform>().rect.yMax); //top-left
        SelectionVertex[1].transform.localPosition = new Vector2(destObj.GetComponent<RectTransform>().rect.xMax, destObj.GetComponent<RectTransform>().rect.yMax); //top-right
        SelectionVertex[2].transform.localPosition = new Vector2(destObj.GetComponent<RectTransform>().rect.xMin, destObj.GetComponent<RectTransform>().rect.yMin); //bottom-left
        SelectionVertex[3].transform.localPosition = new Vector2(destObj.GetComponent<RectTransform>().rect.xMax, destObj.GetComponent<RectTransform>().rect.yMin); //bottom-right
    }

    void ActivateCornerImages()
    {
        for (int i = 0; i < 4; i++)
        {
            SelectionVertex[i].SetActive(true);
        }
    }

    void DeactivateCornerImages()
    {
        for (int i = 0; i < 4; i++)
        {
            SelectionVertex[i].SetActive(false);
        }
    }
}
