using System.Threading.Tasks;
using Game.Core.Drivers;
using Game.Infrastructure.Disposer;
using Game.Infrastructure.Factories.Drivers;
using Game.Infrastructure.Factories.Vehicles;
using Game.Services.SceneLoader;

namespace Game.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IVehicleFactory _vehicleFactory;
        private readonly IDriverFactory _driverFactory;
        private readonly IDisposer _disposer;

        public LoadLevelState(
            IStateMachine stateMachine,
            ISceneLoader sceneLoader,
            IVehicleFactory vehicleFactory,
            IDriverFactory driverFactory,
            IDisposer disposer
        )
        {
            _disposer = disposer;
            _driverFactory = driverFactory;
            _vehicleFactory = vehicleFactory;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _vehicleFactory.Prepare();
            await _sceneLoader.Load(Scene.Core, OnLoaded);
        }

        public void Exit()
        {
            _vehicleFactory.Clear();
        }

        private async void OnLoaded()
        {
            _vehicleFactory.CreateRoot();
            await CreateVehicle<Human>();
            _stateMachine.Enter<GameloopState>();
        }

        private async Task CreateVehicle<T>() where T : Driver
        {
            // var vehicle = await _vehicleFactory.Create();
            // var driver =_driverFactory.Create<T>(vehicle);
            // driver.Initialize();
            // _disposer.Add<Scene>(driver);
        }
    }
}