using System;
using Game.Core.Vehicle;
using Game.Services.Input;
using Zenject;

namespace Game.Core.Drivers
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
        }

        public override void Dispose()
        {
            _inputService.NextGear -= OnNextGear;
            _inputService.PreviousGear -= OnPreviousGear;
            
            _inputService.Acceleration -= OnAcceleration;
            
            _inputService.Acceleration -= OnAcceleration;
        }

        private void OnNextGear() => _vehicle.NextGear();
        private void OnPreviousGear() => _vehicle.PreviousGear();
        private void OnAcceleration(float value) => _vehicle.Accelerate(value);
    }
}