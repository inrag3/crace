namespace Game.Infrastructure.States
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}