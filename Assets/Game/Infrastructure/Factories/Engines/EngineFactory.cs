using System.Collections.Generic;
using Game.Core.Configs;
using Game.Core.Engines;
using Game.Core.Gearboxes;
using Game.Core.Wheels;
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

        public Engine Create(IReadOnlyList<IWheel> wheels, IGearbox gearbox, EngineConfig engineConfig)
        {
            return _container.Instantiate<Engine>(new object[] { wheels, gearbox, engineConfig });
        }
    }
}

