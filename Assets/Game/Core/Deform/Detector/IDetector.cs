using System;

namespace Game.Core.Deform.Detector
{
    public interface IDetector<out T>
    {
        public event Action<T> Detected;
    }
}