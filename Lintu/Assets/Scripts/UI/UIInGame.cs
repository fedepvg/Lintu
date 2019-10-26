using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    #region PublicVariables
    public Text SpeedText;
    public Slider EnergyBar;
    public Image EnergyBarFill;
    public BirdController Player;
    #endregion

    #region PrivateVariables
    float FloorDistance;
    float ActualEnergy;
    #endregion

    void Update()
    {
        if (FloorDistance != Player.FloorDistance)
        {
            FloorDistance = Player.FloorDistance;
            SpeedText.text = FloorDistance.ToString("F2") + " mts";
        }

        EnergyBar.value = Player.Energy;
        if (Player.Energy < 20)
            EnergyBarFill.color = Color.red;
        else
            EnergyBarFill.color = Color.white;
    }
}
