using UnityEngine;

namespace Game.Infrastructure.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        public void Log(string message, object sender = null)
        {
            Debug.Log($"$<b><i>{sender ?? this}</i></b> {message}");
        }
    }
}