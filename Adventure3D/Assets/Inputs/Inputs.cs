// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""695abce9-c014-4d87-9c0e-cff0e3eff6e6"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""573565d4-5ed9-4c98-9a4f-7bdb91adfe78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Change Weapon 1"",
                    ""type"": ""Button"",
                    ""id"": ""2f8e21b1-8a0b-43af-8c75-0d4b0d8bd3dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Change Weapon 2"",
                    ""type"": ""Button"",
                    ""id"": ""68b912ae-09d4-47e4-892d-85ab1ab5b35f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6daf3c9f-af0e-46ad-ae15-c9db8f2d130e"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f660a80e-0cea-4c4a-a84e-63e55761ce01"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change Weapon 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edb93af9-9569-478d-b1b4-24c08dc96cfc"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change Weapon 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_ChangeWeapon1 = m_Gameplay.FindAction("Change Weapon 1", throwIfNotFound: true);
        m_Gameplay_ChangeWeapon2 = m_Gameplay.FindAction("Change Weapon 2", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_ChangeWeapon1;
    private readonly InputAction m_Gameplay_ChangeWeapon2;
    public struct GameplayActions
    {
        private @Inputs m_Wrapper;
        public GameplayActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @ChangeWeapon1 => m_Wrapper.m_Gameplay_ChangeWeapon1;
        public InputAction @ChangeWeapon2 => m_Wrapper.m_Gameplay_ChangeWeapon2;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @ChangeWeapon1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeWeapon1;
                @ChangeWeapon1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeWeapon1;
                @ChangeWeapon1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeWeapon1;
                @ChangeWeapon2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeWeapon2;
                @ChangeWeapon2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeWeapon2;
                @ChangeWeapon2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeWeapon2;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @ChangeWeapon1.started += instance.OnChangeWeapon1;
                @ChangeWeapon1.performed += instance.OnChangeWeapon1;
                @ChangeWeapon1.canceled += instance.OnChangeWeapon1;
                @ChangeWeapon2.started += instance.OnChangeWeapon2;
                @ChangeWeapon2.performed += instance.OnChangeWeapon2;
                @ChangeWeapon2.canceled += instance.OnChangeWeapon2;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnChangeWeapon1(InputAction.CallbackContext context);
        void OnChangeWeapon2(InputAction.CallbackContext context);
    }
}
