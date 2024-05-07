using System.Collections.Generic;
using Game.Core.Configs;
using Game.Core.Engines;
using Game.Core.Gearboxes;
using Game.Core.Wheels;

namespace Game.Infrastructure.Factories.Engines
{
    public interface IEngineFactory
    {
        public Engine Create(IReadOnlyList<IWheel> wheels, IGearbox gearbox, EngineConfig config);
    }
}