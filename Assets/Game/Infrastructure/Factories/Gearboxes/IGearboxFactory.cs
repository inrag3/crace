using Game.Core.Configs;
using Game.Core.Gearboxes;

namespace Game.Infrastructure.Factories.Gearboxes
{
    public interface IGearboxFactory
    {
        public IGearbox Create(GearboxConfig config);
    }
}