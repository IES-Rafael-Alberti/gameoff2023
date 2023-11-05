//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/ShuffleBoard/ShuffleControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ShuffleControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ShuffleControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ShuffleControls"",
    ""maps"": [
        {
            ""name"": ""BoardControls"",
            ""id"": ""8d463674-1460-47e4-a181-2bbb1312aecd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b446a87c-0d1d-49b9-b98e-eb4ebeed6e99"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ClockwiseRotate"",
                    ""type"": ""Button"",
                    ""id"": ""c6af87ad-80dc-48f7-aed7-ca0da1db99a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CounterClockwiseRotate"",
                    ""type"": ""Button"",
                    ""id"": ""44d92ba2-e013-40ea-be3f-a9bc0c3275ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6cf530e4-e905-4ef0-8e87-e51d99554cad"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""36d15736-6016-493c-8703-819739c1e9d2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""76c0c7b2-f92f-4c7b-95e1-518b7600efe7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""56675abf-fbe7-44f5-b64e-1a5f8651690c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""74983feb-e8de-4074-8e60-52c60513bfc4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""23076b39-3337-41d0-aad7-99b52d0a20d5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7d77ca20-a10c-418d-94bd-efcf2e4f923d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClockwiseRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4aef0a3-d43c-40fa-8842-e73133ae0841"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClockwiseRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67887a75-bba5-42b3-b118-2b484a51adfa"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CounterClockwiseRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a20c7883-e95b-47e2-92fa-ecccf29eeba8"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CounterClockwiseRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BoardControls
        m_BoardControls = asset.FindActionMap("BoardControls", throwIfNotFound: true);
        m_BoardControls_Move = m_BoardControls.FindAction("Move", throwIfNotFound: true);
        m_BoardControls_ClockwiseRotate = m_BoardControls.FindAction("ClockwiseRotate", throwIfNotFound: true);
        m_BoardControls_CounterClockwiseRotate = m_BoardControls.FindAction("CounterClockwiseRotate", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // BoardControls
    private readonly InputActionMap m_BoardControls;
    private List<IBoardControlsActions> m_BoardControlsActionsCallbackInterfaces = new List<IBoardControlsActions>();
    private readonly InputAction m_BoardControls_Move;
    private readonly InputAction m_BoardControls_ClockwiseRotate;
    private readonly InputAction m_BoardControls_CounterClockwiseRotate;
    public struct BoardControlsActions
    {
        private @ShuffleControls m_Wrapper;
        public BoardControlsActions(@ShuffleControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_BoardControls_Move;
        public InputAction @ClockwiseRotate => m_Wrapper.m_BoardControls_ClockwiseRotate;
        public InputAction @CounterClockwiseRotate => m_Wrapper.m_BoardControls_CounterClockwiseRotate;
        public InputActionMap Get() { return m_Wrapper.m_BoardControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BoardControlsActions set) { return set.Get(); }
        public void AddCallbacks(IBoardControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_BoardControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BoardControlsActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @ClockwiseRotate.started += instance.OnClockwiseRotate;
            @ClockwiseRotate.performed += instance.OnClockwiseRotate;
            @ClockwiseRotate.canceled += instance.OnClockwiseRotate;
            @CounterClockwiseRotate.started += instance.OnCounterClockwiseRotate;
            @CounterClockwiseRotate.performed += instance.OnCounterClockwiseRotate;
            @CounterClockwiseRotate.canceled += instance.OnCounterClockwiseRotate;
        }

        private void UnregisterCallbacks(IBoardControlsActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @ClockwiseRotate.started -= instance.OnClockwiseRotate;
            @ClockwiseRotate.performed -= instance.OnClockwiseRotate;
            @ClockwiseRotate.canceled -= instance.OnClockwiseRotate;
            @CounterClockwiseRotate.started -= instance.OnCounterClockwiseRotate;
            @CounterClockwiseRotate.performed -= instance.OnCounterClockwiseRotate;
            @CounterClockwiseRotate.canceled -= instance.OnCounterClockwiseRotate;
        }

        public void RemoveCallbacks(IBoardControlsActions instance)
        {
            if (m_Wrapper.m_BoardControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBoardControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_BoardControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BoardControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BoardControlsActions @BoardControls => new BoardControlsActions(this);
    public interface IBoardControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnClockwiseRotate(InputAction.CallbackContext context);
        void OnCounterClockwiseRotate(InputAction.CallbackContext context);
    }
}