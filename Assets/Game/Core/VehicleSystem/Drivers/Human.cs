using System;
using Game.Core.VehicleSystem.Vehicle;
using Game.Services.Input;

namespace Game.Core.VehicleSystem.Drivers
{
    public class Human : Driver, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly IVehicle _vehicle;

        public Human(IInputService inputService, IVehicle vehicle)
        {
            _vehicle = vehicle;
            _inputService = inputService;
        }

        public override void Initialize()
        {
            _inputService.NextGear += OnNextGear;
            _inputService.PreviousGear += OnPreviousGear;
            
            _inputService.Acceleration += OnAcceleration;
            
            _inputService.Braking += OnBraking;
            
            _inputService.Steering += OnSteering;
            
            _inputService.HandBrake += OnHandBrake;
        }

        public override void Dispose()
        {
            _inputService.NextGear -= OnNextGear;
            _inputService.PreviousGear -= OnPreviousGear;
            
            _inputService.Acceleration -= OnAcceleration;
            
            _inputService.Braking -= OnBraking;
            
            _inputService.Steering -= OnSteering;
        }

        private void OnNextGear() => _vehicle.NextGear();
        private void OnHandBrake(bool value) => _vehicle.HandBrake(value);
        private void OnSteering(float value) => _vehicle.Steering(value);
        private void OnBraking(float value) => _vehicle.Brake(value);
        private void OnPreviousGear() => _vehicle.PreviousGear();
        private void OnAcceleration(float value) => _vehicle.Accelerate(value);
    }
}