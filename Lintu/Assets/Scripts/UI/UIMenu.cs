using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIMenu : MonoBehaviour
{
    public Text VersionText;

    private void Start()
    {
        VersionText.text = "v" + Application.version;
    }

    private void Update()
    {
        if (!EventSystem.current.currentSelectedGameObject && GameManager.Instance.GameInput.UI.Navigate.triggered)
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }


}
