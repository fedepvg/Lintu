using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    public Text VersionText;

    void Start()
    {
        VersionText.text = "v" + Application.version;
    }
}
