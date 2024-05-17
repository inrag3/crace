using UnityEngine;

namespace Game.Core.VehicleSystem.Wheels
{
    public interface IWheel
    {
        public float RotationPerMinute { get; }
        public bool IsGrounded { get; }
        public bool IsDrive { get; }
        public void Brake(float value);
        public void MotorTorque(float value);
        public Suspension Suspension { get; } 
        public WheelCollider Collider { get; }
    }
}