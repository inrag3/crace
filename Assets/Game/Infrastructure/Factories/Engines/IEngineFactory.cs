using System.Collections.Generic;
using Game.Core.VehicleSystem.Configs;
using Game.Core.VehicleSystem.Engines;
using Game.Core.VehicleSystem.Gearboxes;
using Game.Core.VehicleSystem.Wheels;
using PG;

namespace Game.Infrastructure.Factories.Engines
{
    public interface IEngineFactory
    {
        public Engine Create(IWheel[] wheels, IGearbox gearbox, EngineConfig config);
    }
}