using Game.Services.SceneLoader;

namespace Game.Infrastructure.States
{
    public class MetaState : IState
    {
        private readonly ISceneLoader _sceneLoader;

        
        public MetaState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public async void Enter()
        {
            await _sceneLoader.Load(Scene.Meta);
        }

        public void Exit()
        {
        
        }
    }
}