namespace Game.Infrastructure.States
{
    public interface IStateMachine
    {
        public void Enter<T>() where T : IState;
    }
}