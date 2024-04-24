using UnityEngine;

namespace Game.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        public void Log(string message, object sender = null)
        {
            sender ??= this;
            Debug.Log($"<b><i>{sender.GetType().Name}:</i></b> {message}");
        }
    }
}