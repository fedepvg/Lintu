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
                    ""name"": ""Accelerate"",
                    ""type"": ""Button"",
                    ""id"": ""f8d40ca0-4b45-4f45-95e8-988030503a98"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""50a807cf-d1ce-452a-a37b-d60fcff2e678"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Deaccelerate"",
                    ""type"": ""Button"",
                    ""id"": ""fea671da-b9d3-4128-a488-1e8cedd58d86"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""3fdb2eba-5729-4926-b208-891bf852ebfc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftRoll"",
                    ""type"": ""Button"",
                    ""id"": ""73f4611f-6390-45d4-86cc-8e5b37e68adb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightRoll"",
                    ""type"": ""Button"",
                    ""id"": ""4d1a99ca-e754-4947-a2e6-0358f787de6a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b971bf77-0461-408b-a8aa-9ac82476dcdb"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a52bc97-1f00-4df8-bb6f-4381ccaa7532"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""911fcff9-be71-4613-956c-4b449bc95ea8"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e98ff83-dfae-4666-a9de-409ba5d9a714"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Deaccelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63a6ed68-3528-41f3-8ea7-ff95c3b21225"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""035c40a9-aba7-426d-b836-a151e3072dfc"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e8ca655-78c6-42ce-a001-70479455e8cb"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.GetActionMap("Gameplay");
        m_Gameplay_Accelerate = m_Gameplay.GetAction("Accelerate");
        m_Gameplay_Rotate = m_Gameplay.GetAction("Rotate");
        m_Gameplay_Deaccelerate = m_Gameplay.GetAction("Deaccelerate");
        m_Gameplay_Dash = m_Gameplay.GetAction("Dash");
        m_Gameplay_LeftRoll = m_Gameplay.GetAction("LeftRoll");
        m_Gameplay_RightRoll = m_Gameplay.GetAction("RightRoll");
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
    private readonly InputAction m_Gameplay_Accelerate;
    private readonly InputAction m_Gameplay_Rotate;
    private readonly InputAction m_Gameplay_Deaccelerate;
    private readonly InputAction m_Gameplay_Dash;
    private readonly InputAction m_Gameplay_LeftRoll;
    private readonly InputAction m_Gameplay_RightRoll;
    public struct GameplayActions
    {
        private PlayerControlsPS4 m_Wrapper;
        public GameplayActions(PlayerControlsPS4 wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accelerate => m_Wrapper.m_Gameplay_Accelerate;
        public InputAction @Rotate => m_Wrapper.m_Gameplay_Rotate;
        public InputAction @Deaccelerate => m_Wrapper.m_Gameplay_Deaccelerate;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputAction @LeftRoll => m_Wrapper.m_Gameplay_LeftRoll;
        public InputAction @RightRoll => m_Wrapper.m_Gameplay_RightRoll;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                Accelerate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAccelerate;
                Accelerate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAccelerate;
                Accelerate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAccelerate;
                Rotate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                Rotate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                Rotate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                Deaccelerate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDeaccelerate;
                Deaccelerate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDeaccelerate;
                Deaccelerate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDeaccelerate;
                Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                LeftRoll.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftRoll;
                LeftRoll.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftRoll;
                LeftRoll.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftRoll;
                RightRoll.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightRoll;
                RightRoll.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightRoll;
                RightRoll.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightRoll;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Accelerate.started += instance.OnAccelerate;
                Accelerate.performed += instance.OnAccelerate;
                Accelerate.canceled += instance.OnAccelerate;
                Rotate.started += instance.OnRotate;
                Rotate.performed += instance.OnRotate;
                Rotate.canceled += instance.OnRotate;
                Deaccelerate.started += instance.OnDeaccelerate;
                Deaccelerate.performed += instance.OnDeaccelerate;
                Deaccelerate.canceled += instance.OnDeaccelerate;
                Dash.started += instance.OnDash;
                Dash.performed += instance.OnDash;
                Dash.canceled += instance.OnDash;
                LeftRoll.started += instance.OnLeftRoll;
                LeftRoll.performed += instance.OnLeftRoll;
                LeftRoll.canceled += instance.OnLeftRoll;
                RightRoll.started += instance.OnRightRoll;
                RightRoll.performed += instance.OnRightRoll;
                RightRoll.canceled += instance.OnRightRoll;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnAccelerate(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnDeaccelerate(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnLeftRoll(InputAction.CallbackContext context);
        void OnRightRoll(InputAction.CallbackContext context);
    }
}
