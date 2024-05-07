namespace Game.Core.Wheels
{
    public interface IWheel
    {
        public float RotationPerMinute { get; }
        public bool IsGrounded { get; }
        public bool IsDrive { get; }
        public void Brake(float value);
        public void MotorTorque(float value);
    }
}