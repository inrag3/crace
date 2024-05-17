using Game.Core.VehicleSystem.Drivers;
using Game.Core.VehicleSystem.Vehicle;

namespace Game.Infrastructure.Factories.Drivers
{
    public interface IDriverFactory
    {
        public T Create<T>(IVehicle vehicle) where T : Driver;
    }
}