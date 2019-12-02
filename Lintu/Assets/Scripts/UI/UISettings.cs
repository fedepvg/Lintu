using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public Toggle InvertedYToggle;
    public Slider VolumeSlider;

    bool CurrentToggleValue;

    void Start()
    {
        InvertedYToggle.isOn = GameManager.Instance.InvertedY;
        CurrentToggleValue = InvertedYToggle.isOn;
    }

    void Update()
    {
        if (CurrentToggleValue != InvertedYToggle.isOn)
        {
            CurrentToggleValue = InvertedYToggle.isOn;
            GameManager.Instance.InvertedY = CurrentToggleValue;
        }
    }
}
