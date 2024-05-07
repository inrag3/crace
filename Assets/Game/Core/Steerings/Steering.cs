using Game.Services.Logger;
using Zenject;

namespace Game.Core.Steerings
{
    public class Steering : ISteering, IFixedTickable
    {
        private ILoggerService _logger;
        public float Steer { get; private set; }

        [Inject]
        private void Construct(ILoggerService logger)
        {
            _logger = logger;
        }
        
        public void FixedTick()
        {
            
        }

        public void SetSteer(float value)
        {
            if (value is < -1 or > 1)
                _logger.Error($"Steering value should be between -1 and 1. Value: {value}", this);
            Steer = value;
        }
    }
}