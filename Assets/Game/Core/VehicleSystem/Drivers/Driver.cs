using System;
using Zenject;

namespace Game.Core.VehicleSystem.Drivers
{
    public abstract class Driver : IInitializable, IDisposable
    {
        public abstract void Dispose();
        public abstract void Initialize();
    }
}