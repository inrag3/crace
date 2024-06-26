namespace Game.Services.Logger
{
    public interface ILoggerService
    {
        public void Log(string message, object sender = null);
        public void Error(string message, object sender = null);
    }
}