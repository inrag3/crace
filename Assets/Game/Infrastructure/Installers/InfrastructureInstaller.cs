using Game.Infrastructure.Services.Logger;
using Zenject;

namespace Game.Infrastructure.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLoggerService();
        }

        private void BindLoggerService()
        {
            Container.BindInterfacesAndSelfTo<LoggerService>().AsSingle().NonLazy();
        }
    }
}