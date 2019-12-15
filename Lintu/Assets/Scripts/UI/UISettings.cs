using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public Toggle InvertedYToggle;
    public Toggle HUDToggle;
    public Slider VolumeSlider;
    public AK.Wwise.RTPC VolumeRTPCParameter;

    bool CurrentYToggleValue;
    bool CurrentHUDToggleValue;

    void Start()
    {
        InvertedYToggle.isOn = GameManager.Instance.InvertedY;
        CurrentYToggleValue = InvertedYToggle.isOn;
        HUDToggle.isOn = GameManager.Instance.HUD;
        CurrentHUDToggleValue = HUDToggle.isOn;
        VolumeSlider.value = VolumeRTPCParameter.GetGlobalValue();
    }

    void Update()
    {
        if (CurrentYToggleValue != InvertedYToggle.isOn)
        {
            CurrentYToggleValue = InvertedYToggle.isOn;
            GameManager.Instance.InvertedY = CurrentYToggleValue;
        }
        
        if (CurrentHUDToggleValue != HUDToggle.isOn)
        {
            CurrentHUDToggleValue = HUDToggle.isOn;
            GameManager.Instance.HUD = CurrentHUDToggleValue;
        }
    }

    public void ModifyVolume()
    {
        VolumeRTPCParameter.SetGlobalValue(VolumeSlider.value);
    }
}
