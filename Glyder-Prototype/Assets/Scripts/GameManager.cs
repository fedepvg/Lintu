using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<PlayerControlsPS4> PS4Contollers;

    private void Awake()
    {
        PlayerControlsPS4 P1 = new PlayerControlsPS4();
        PlayerControlsPS4 P2 = new PlayerControlsPS4();

        PS4Contollers = new List<PlayerControlsPS4>();
        PS4Contollers.Add(P1);
        PS4Contollers.Add(P2);

        
    }
}
