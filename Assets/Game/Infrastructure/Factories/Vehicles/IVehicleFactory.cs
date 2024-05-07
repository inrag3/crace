using System.Threading.Tasks;
using Game.Core.Vehicle;
using Game.Infrastructure.States;

namespace Game.Infrastructure.Factories.Vehicles
{
    public interface IVehicleFactory : IAsyncFactory
    {
        public void CreateRoot();
        public Task<IVehicle> Create();
    }
}