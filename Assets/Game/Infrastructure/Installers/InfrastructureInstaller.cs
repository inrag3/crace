using Game.Infrastructure.AssetManagement;
using Game.Infrastructure.Factories;
using Game.Infrastructure.Services.CoroutinePerformer;
using Game.Infrastructure.Services.Logger;
using Game.Infrastructure.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private CoroutinePerformer _coroutinePerformer;
        private void OnValidate()
        {
            _coroutinePerformer ??= GetComponent<CoroutinePerformer>();
        }

        public override void InstallBindings()
        {
            BindAssetProvider();
            BindServices();
            BindFactories();
            BindSceneLoader();
        }

        private void BindAssetProvider()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle().NonLazy();
        }
        private void BindServices()
        {
            Container.Bind<ILoggerService>().To<LoggerService>().AsSingle().NonLazy();
            Container.Bind<ICoroutinePerformer>().FromInstance(_coroutinePerformer).AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.Bind<StateFactory>().AsSingle().NonLazy();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}