// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControlsPS4.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControlsPS4 : IInputActionCollection
{
    private InputActionAsset asset;
    public PlayerControlsPS4()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControlsPS4"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""fc259474-d6af-4d86-b764-6b9312e92bd6"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e0034ca4-b9b0-407f-8c27-243a0b3e9559"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Button"",
                    ""id"": ""96b0c4d5-b59a-4fe8-801f-2e005b721486"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ae341c0c-ebae-4a82-a2a6-08fa232193a3"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f916cb2d-9275-4a05-a186-d1b759d65c42"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f30e5135-00f0-446d-83c6-545d29ef161e"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""29929091-88a5-41cc-b6ce-50ab24f087ec"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5a5dd848-e873-400e-9db7-507caf52319f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e022228c-0610-49dc-9fe3-41f1d7a93532"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.GetActionMap("Gameplay");
        m_Gameplay_Jump = m_Gameplay.GetAction("Jump");
        m_Gameplay_Horizontal = m_Gameplay.GetAction("Horizontal");
    }

    ~PlayerControlsPS4()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Horizontal;
    public struct GameplayActions
    {
        private PlayerControlsPS4 m_Wrapper;
        public GameplayActions(PlayerControlsPS4 wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Horizontal => m_Wrapper.m_Gameplay_Horizontal;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Horizontal.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHorizontal;
                Horizontal.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHorizontal;
                Horizontal.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHorizontal;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Horizontal.started += instance.OnHorizontal;
                Horizontal.performed += instance.OnHorizontal;
                Horizontal.canceled += instance.OnHorizontal;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnHorizontal(InputAction.CallbackContext context);
    }
}
