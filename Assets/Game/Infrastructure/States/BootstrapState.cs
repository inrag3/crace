namespace Game.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;

        public BootstrapState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            //TODO: async services initialization
            _stateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
        }
    }
}