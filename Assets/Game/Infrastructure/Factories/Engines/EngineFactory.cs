using System.Collections.Generic;
using Game.Core.VehicleSystem.Configs;
using Game.Core.VehicleSystem.Engines;
using Game.Core.VehicleSystem.Gearboxes;
using Game.Core.VehicleSystem.Wheels;
using PG;
using Zenject;

namespace Game.Infrastructure.Factories.Engines
{
    public class EngineFactory : IEngineFactory
    {
        private readonly DiContainer _container;

        public EngineFactory(DiContainer container)
        {
            _container = container;
        }

        public Engine Create(IWheel[] wheels, IGearbox gearbox, EngineConfig engineConfig)
        {
            return _container.Instantiate<Engine>(new object[] { wheels, gearbox, engineConfig });
        }
    }
}

