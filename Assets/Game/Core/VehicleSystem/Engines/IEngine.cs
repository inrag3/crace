using Zenject;

namespace Game.Core.VehicleSystem.Engines
{
    public interface IEngine : IFixedTickable
    {
        public bool IsOn { get; }
        public float RevolutionsPerMinute { get; }
        public float Acceleration { get; }
        public float MaxTorque { get; }
        public float Torque { get; }
        public void SetAcceleration(float value);
        public void Start();
        public void Stop();
    }
}