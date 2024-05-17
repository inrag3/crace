using Zenject;

namespace Game.Core.VehicleSystem.Steerings
{
    public interface ISteering : IFixedTickable
    {
        public float Steer { get; }

        public void SetSteer(float value);
    }
}