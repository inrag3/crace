using UnityEngine;

namespace Game.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        public void Log(string message, object sender = null)
        {
            sender ??= this;
            Debug.Log(Decorate(message, sender));
        }

        public void Error(string message, object sender = null)
        {
            sender ??= this;
            Debug.LogError(Decorate(message, sender));
        }

        private string Decorate(string message, object sender) => 
            $"<b><i>{sender.GetType().Name}:</i></b> {message}";
    }
}