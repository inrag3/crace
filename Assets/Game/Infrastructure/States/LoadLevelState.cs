using Game.Infrastructure.Services.SceneLoader;

namespace Game.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _loader;

        public LoadLevelState(IStateMachine stateMachine, ISceneLoader loader)
        {
            _stateMachine = stateMachine;
            _loader = loader;
        }
        
        public void Enter()
        {
            _loader.Load(Scene.Main, OnLoaded);
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