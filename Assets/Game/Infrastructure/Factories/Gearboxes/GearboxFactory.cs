using Game.Core.Configs;
using Game.Core.Gearboxes;
using Zenject;

namespace Game.Infrastructure.Factories.Gearboxes
{
    public class GearboxFactory : IGearboxFactory
    {
        private readonly DiContainer _container;

        public GearboxFactory(DiContainer container)
        {
            _container = container;
        }
        public IGearbox Create(GearboxConfig config)
        {
            return _container.Instantiate<Gearbox>(new[] { config });
        }
    }
}