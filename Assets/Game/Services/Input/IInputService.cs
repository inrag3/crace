using System;

namespace Game.Services.Input
{
    public interface IInputService
    {
        public event Action<float> Acceleration;
        public event Action<float> Braking;
        public event Action<float> Steering;
        public event Action<bool> HandBrake;
        public event Action NextGear;
        public event Action PreviousGear;
    }
}