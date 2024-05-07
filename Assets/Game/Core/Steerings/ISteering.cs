namespace Game.Core.Steerings
{
    public interface ISteering
    {
        public float Steer { get; }

        public void SetSteer(float value);
    }
}