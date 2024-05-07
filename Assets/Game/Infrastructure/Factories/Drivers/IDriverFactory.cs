using Game.Core.Drivers;
using Game.Core.Vehicle;

namespace Game.Infrastructure.Factories.Drivers
{
    public interface IDriverFactory
    {
        public T Create<T>(IVehicle vehicle) where T : Driver;
    }
}