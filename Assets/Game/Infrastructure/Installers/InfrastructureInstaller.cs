using Game.Infrastructure.AssetManagement;
using Game.Infrastructure.Factories;
using Game.Infrastructure.Factories.Drivers;
using Game.Infrastructure.Factories.Engines;
using Game.Infrastructure.Factories.Gearboxes;
using Game.Infrastructure.Factories.Vehicles;
using Game.Services.CoroutinePerformer;
using Game.Services.Input;
using Game.Services.Logger;
using Game.Services.SceneLoader;
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
            BindServices();
            BindFactories();
            BindInput();
            BindDisposer();
        }
        
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle().NonLazy();
            Container.Bind<ISceneLoader>().To<AsyncSceneLoader>().AsSingle();
            Container.Bind<ILoggerService>().To<LoggerService>().AsSingle().NonLazy();
            Container.Bind<ICoroutinePerformer>().FromInstance(_coroutinePerformer).AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.Bind<StateFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VehicleFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<DriverFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EngineFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GearboxFactory>().AsSingle();
        }
        
        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
        }

        private void BindDisposer()
        {
            Container.BindInterfacesTo<Disposer.Disposer>().AsSingle().NonLazy();
        }
    }
}