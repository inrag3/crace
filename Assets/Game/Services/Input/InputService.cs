using System;
using Game.Services.Logger;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Services.Input
{
    public class InputService : IInputService, IDisposable, IInitializable
    {
        private readonly Controls _controls;
        private readonly ILoggerService _logger;
        public event Action<float> Acceleration;
        public event Action<float> Braking;
        public event Action<float> Steering;
        public event Action<bool> HandBrake;
        public event Action NextGear;
        public event Action PreviousGear;

        public InputService(ILoggerService logger)
        {
            _logger = logger;
            _controls = new Controls();
            _controls.Enable();
            _logger.Log("enabled", this);
        }

        public void Initialize() => 
            Subscribe();

        public void Dispose()
        {
            Unsubscribe();
            _controls.Disable();
            _controls.Dispose();
            _logger.Log("disposed", this);
        }

        private void Subscribe()
        {
            _controls.Driver.Acceleration.performed += OnAcceleration;
            _controls.Driver.Acceleration.canceled += OnAcceleration;
            
            _controls.Driver.Braking.performed += OnBraking;
            _controls.Driver.Braking.canceled += OnBraking;
            
            _controls.Driver.Steering.performed += OnSteering;
            _controls.Driver.Steering.canceled += OnSteering;
            
            _controls.Driver.HandBrake.started += OnHandBrake;
            
            _controls.Driver.NextGear.performed += OnNextGear;
            _controls.Driver.PreviousGear.performed += OnPreviousGear;
        }

        private void OnAcceleration(InputAction.CallbackContext context) => 
            Acceleration?.Invoke(context.ReadValue<float>());

        private void OnBraking(InputAction.CallbackContext context) => 
            Braking?.Invoke(context.ReadValue<float>());

        private void OnSteering(InputAction.CallbackContext context) => 
            Steering?.Invoke(context.ReadValue<float>());

        private void OnHandBrake(InputAction.CallbackContext context) =>
            HandBrake?.Invoke(context.phase == InputActionPhase.Started);

        private void OnNextGear(InputAction.CallbackContext context) => 
            NextGear?.Invoke();

        private void OnPreviousGear(InputAction.CallbackContext context) => 
            PreviousGear?.Invoke();

        private void Unsubscribe()
        {
            _controls.Driver.Acceleration.performed -= OnAcceleration;
            _controls.Driver.Acceleration.canceled -= OnAcceleration;
            
            _controls.Driver.Braking.performed -= OnBraking;
            _controls.Driver.Braking.canceled -= OnBraking;
            
            _controls.Driver.Steering.performed -= OnSteering;
            _controls.Driver.Steering.canceled -= OnSteering;
            
            _controls.Driver.HandBrake.started -= OnHandBrake;
            
            _controls.Driver.NextGear.performed -= OnNextGear;
            _controls.Driver.PreviousGear.performed -= OnPreviousGear;
        }
    }
}