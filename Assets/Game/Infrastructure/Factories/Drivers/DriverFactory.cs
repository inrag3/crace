using Game.Core.VehicleSystem.Drivers;
using Game.Core.VehicleSystem.Vehicle;
using Zenject;

namespace Game.Infrastructure.Factories.Drivers
{
    public class DriverFactory : IDriverFactory
    {
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
        public T Create<T>(IVehicle vehicle) where T : Driver
        {
            return _container.Instantiate<T>(new[] { vehicle });
        }
    }
}