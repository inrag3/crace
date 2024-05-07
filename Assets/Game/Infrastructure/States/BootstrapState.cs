using Game.Services.SceneLoader;

namespace Game.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }
        
        public async void Enter()
        {
           await _sceneLoader.Load(Scene.Bootstrap, onLoaded: OnLoaded);
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<MetaState>();
        }

        public void Exit()
        {
        }
    }
}
