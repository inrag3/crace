using System;
using Game.Services.Logger;
using Unity.VisualScripting;

namespace Game.Services.Input
{
    public class InputService : IInputService, IDisposable, IInitializable
    {
        private readonly Controls _controls;
        private readonly ILoggerService _logger;
        public float Acceleration { get; private set; }
        public InputService(ILoggerService logger)
        {
            _logger = logger;
            _controls = new Controls();
            _controls.Enable();
        }

        public void Initialize() => 
            Subscribe();

        public void Dispose()
        {
            Unsubscribe();
            _controls.Disable();
            _logger.Log("disposed", this);
        }
        
        private void Subscribe()
        {
            
        }

        private void Unsubscribe()
        {
            
        }
    }

    public interface IInputService
    {
        
    }
}