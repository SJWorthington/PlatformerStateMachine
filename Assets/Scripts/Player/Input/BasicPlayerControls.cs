// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/Input/BasicPlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @BasicPlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @BasicPlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BasicPlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""795d0298-c15d-43e0-adf8-3f4c9dd9d183"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""bae0a9c7-e899-426a-81b8-f61a4ee7c7f4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""fdf655f0-2bf4-4583-af53-b634e772151a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Bash"",
                    ""type"": ""PassThrough"",
                    ""id"": ""717c6cc9-cefc-427f-bf81-59d1506fb0a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BashAim"",
                    ""type"": ""Value"",
                    ""id"": ""3b0d66d2-b9e0-4c12-8984-3c3b660df9c6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwapLayer"",
                    ""type"": ""Button"",
                    ""id"": ""b0a8cfd6-600c-431e-933b-ba8f7a658cbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""134ce29d-03e0-4ca3-b3dc-933569750c67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""da87f4b1-995f-4dc3-a2af-e589d43b9ede"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88198967-8597-4248-8074-930142ea70b1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f152cd4d-f92e-4f47-9a8b-913f8eee604a"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Bash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""842c6c75-b979-41c1-8a0c-fc25fc6dc33f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BashAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""741ff43d-9dd2-4a41-ad25-e9f3c04eb9c3"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapLayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad5cf562-c08c-4291-9379-0f35faf573f2"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Bash = m_Gameplay.FindAction("Bash", throwIfNotFound: true);
        m_Gameplay_BashAim = m_Gameplay.FindAction("BashAim", throwIfNotFound: true);
        m_Gameplay_SwapLayer = m_Gameplay.FindAction("SwapLayer", throwIfNotFound: true);
        m_Gameplay_Grab = m_Gameplay.FindAction("Grab", throwIfNotFound: true);
    }

    public void Dispose()
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
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Bash;
    private readonly InputAction m_Gameplay_BashAim;
    private readonly InputAction m_Gameplay_SwapLayer;
    private readonly InputAction m_Gameplay_Grab;
    public struct GameplayActions
    {
        private @BasicPlayerControls m_Wrapper;
        public GameplayActions(@BasicPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Bash => m_Wrapper.m_Gameplay_Bash;
        public InputAction @BashAim => m_Wrapper.m_Gameplay_BashAim;
        public InputAction @SwapLayer => m_Wrapper.m_Gameplay_SwapLayer;
        public InputAction @Grab => m_Wrapper.m_Gameplay_Grab;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Bash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBash;
                @Bash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBash;
                @Bash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBash;
                @BashAim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBashAim;
                @BashAim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBashAim;
                @BashAim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBashAim;
                @SwapLayer.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapLayer;
                @SwapLayer.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapLayer;
                @SwapLayer.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapLayer;
                @Grab.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Bash.started += instance.OnBash;
                @Bash.performed += instance.OnBash;
                @Bash.canceled += instance.OnBash;
                @BashAim.started += instance.OnBashAim;
                @BashAim.performed += instance.OnBashAim;
                @BashAim.canceled += instance.OnBashAim;
                @SwapLayer.started += instance.OnSwapLayer;
                @SwapLayer.performed += instance.OnSwapLayer;
                @SwapLayer.canceled += instance.OnSwapLayer;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnBash(InputAction.CallbackContext context);
        void OnBashAim(InputAction.CallbackContext context);
        void OnSwapLayer(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
    }
}
