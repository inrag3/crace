using Game.Infrastructure.Disposer;
using Game.Services.SceneLoader;

namespace Game.Infrastructure.States
{
    public class GameloopState : IState
    {
        private readonly IDisposer _disposer;

        public GameloopState(IDisposer disposer)
        {
            _disposer = disposer;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            _disposer.Dispose<Scene>();
        }
    }
}
