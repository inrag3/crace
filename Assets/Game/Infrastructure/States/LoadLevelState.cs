using Game.Infrastructure.Services.SceneLoader;

namespace Game.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public async void Enter()
        {
            await _sceneLoader.Load(Scene.Core, OnLoaded);
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameloopState>();
        }
        
        public void Exit()
        {
        }
    }
}