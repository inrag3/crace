using UnityEngine;

namespace Game.Core.VehicleSystem.Wheels
{
    public class Suspension
    {
        private readonly ISuspendable _suspendable;
        public float Position { get; private set; }

        public Suspension(ISuspendable suspendable)
        {
            _suspendable = suspendable;
        }

        public void Calculate(float localPosition)
        {
            Position = Mathf.InverseLerp(_suspendable.Center.y - _suspendable.SuspensionDistance,
                _suspendable.Center.y, localPosition);
        }
    }
}