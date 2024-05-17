using System.Threading.Tasks;
using Game.Core.AI;
using Game.Core.VehicleSystem.Vehicle;
using Game.Infrastructure.States;

namespace Game.Infrastructure.Factories.Vehicles
{
    public interface IVehicleFactory : IAsyncFactory
    {
        public void CreateRoot();
        public Task<IVehicle> Create();
        public Task<IVehicle> CreateBot();
    }
}