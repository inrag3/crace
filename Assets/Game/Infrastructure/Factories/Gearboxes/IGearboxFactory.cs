using Game.Core.VehicleSystem.Configs;
using Game.Core.VehicleSystem.Gearboxes;
using PG;
using GearboxConfig = Game.Core.VehicleSystem.Configs.GearboxConfig;

namespace Game.Infrastructure.Factories.Gearboxes
{
    public interface IGearboxFactory
    {
        public IGearbox Create(GearboxConfig config);
    }
}