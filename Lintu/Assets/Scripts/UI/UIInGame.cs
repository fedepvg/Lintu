using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIInGame : MonoBehaviour
{
    #region PublicVariables
    public Slider EnergyBar;
    public Image EnergyBarFill;
    public Slider DistanceBar;
    public BirdController Player;
    public GameObject PausePanel;
    public Transform StartTransform;
    public Transform FinishTransform;
    public Sprite LintuUI1;
    public Sprite LintuUI2;
    public float DistanceHandleAnimationTime;
    public Image DistanceBarHandle;
    #endregion

    float SpriteChangeTimer;
    float LevelDistance;
    Sprite CurrentDistanceHandleSprite;
    UIPause PauseScript;
    bool HUDActive;

    private void Start()
    {
        GameManager.Instance.GameInput.Gameplay.Pause.performed += ctx => SetPauseState();
        LevelDistance = FinishTransform.position.z - StartTransform.position.z;
        SpriteChangeTimer = 0;
        CurrentDistanceHandleSprite = LintuUI1;
        PausePanel.SetActive(false);
        PauseScript = PausePanel.GetComponent<UIPause>();
        HUDActive = GameManager.Instance.HUD;
        if (!HUDActive)
            DeactivateHUD();
        else
            ActivateHUD();

        BirdController.EndLevelAction += DestroyHUD;
    }

    void Update()
    {
        EnergyBar.value = Player.Energy;
        if (Player.Energy < 20)
            EnergyBarFill.color = Color.red;
        else
            EnergyBarFill.color = Color.white;

        DistanceBar.value = Player.LevelDistanceLeft * 100 / LevelDistance;
        SpriteChangeTimer += Time.deltaTime;
        if(SpriteChangeTimer>=DistanceHandleAnimationTime)
        {
            SpriteChangeTimer = 0f;
            if (CurrentDistanceHandleSprite == LintuUI1)
                CurrentDistanceHandleSprite = LintuUI2;
            else
                CurrentDistanceHandleSprite = LintuUI1;
            DistanceBarHandle.sprite = CurrentDistanceHandleSprite;
        }

        if (HUDActive != GameManager.Instance.HUD)
        {
            ChangeHUDState();
            HUDActive = GameManager.Instance.HUD;
        }

        if (!EventSystem.current.currentSelectedGameObject && PausePanel.activeSelf && GameManager.Instance.GameInput.UI.Navigate.triggered)
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        else if (!PausePanel.activeSelf)
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void SetPauseState()
    {
        if (PausePanel)
        {
            if (!PauseScript.OnSettings)
            {
                PauseScript.ResetPanels();
                PausePanel.SetActive(!PausePanel.activeSelf);
                if (PausePanel.activeSelf)
                {
                    EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
                    AkSoundEngine.PostEvent("Pausa_On", gameObject);
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    AkSoundEngine.PostEvent("Pausa_Off", gameObject);
                }

                Player.enabled = !Player.enabled;
                Time.timeScale = Mathf.Abs(Time.timeScale - 1);
            }
            else
            {
                PauseScript.SwitchPanels();
            }
        }
    }

    public void ChangeHUDState()
    {
        EnergyBar.gameObject.SetActive(!EnergyBar.gameObject.activeSelf);
        DistanceBar.gameObject.SetActive(!DistanceBar.gameObject.activeSelf);
    }

    public void DeactivateHUD()
    {
        EnergyBar.gameObject.SetActive(false);
        DistanceBar.gameObject.SetActive(false);
    }
    
    public void ActivateHUD()
    {
        EnergyBar.gameObject.SetActive(true);
        DistanceBar.gameObject.SetActive(true);
    }

    void DestroyHUD()
    {
        BirdController.EndLevelAction -= DestroyHUD;
        Destroy(this);
    }
}
