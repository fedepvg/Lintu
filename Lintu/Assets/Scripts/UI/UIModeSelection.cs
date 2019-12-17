using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIModeSelection : MonoBehaviour
{
    public GameObject BestTimeGO;
    public TextMeshProUGUI BestTimeText;

    void Start()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            float bestTime = PlayerPrefs.GetFloat("BestTime");
            int minutes = TimeToMinutes(bestTime);
            int seconds = TimeToSeconds(bestTime);
            BestTimeText.text= string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
            BestTimeGO.SetActive(false);
    }
   
    int TimeToSeconds(float t)
    {
        return (int)(t % 60);
    }

    int TimeToMinutes(float t)
    {
        return (int)(t / 60);
    }
}
