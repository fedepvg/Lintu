using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public PlayerControls GameInput;

    public override void Awake()
    {
        base.Awake();
        GameInput = new PlayerControls();
        GameInput.Enable();
    }

    public PlayerControls Input
    {
        get { return GameInput; }
    }
}
