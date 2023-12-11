using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace STGO.Input
{
    public class PlayerInputHandler
    {
        public InputData InputData => GetInputData();

#if ENABLE_INPUT_SYSTEM
        private PlayerInputActions _actions;
        private InputAction _shoot;

        public PlayerInputHandler()
        {
            _actions = new PlayerInputActions();
            _actions.Enable();
            _shoot = _actions.Player.Shoot;
        }

        public void Enable()
        {
            _actions.Enable();
        }

        public void Disable()
        {
            _actions.Disable();
        }

        private InputData GetInputData()
        {
            return new InputData
            {
                IsShootDown = _shoot.WasPressedThisFrame(),
                IsShootHeld = _shoot.IsPressed()
            };
        }
#elif ENABLE_LEGACY_INPUT_MANAGER
        private InputData GetInputData()
        {
            return new InputData
            {
                JumpDown = Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0),
                JumpHeld = Input.GetButton("Fire1") || Input.GetMouseButton(0),
            };
        }
#endif
    }
}