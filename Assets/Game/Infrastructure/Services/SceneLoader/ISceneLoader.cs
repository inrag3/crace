using System;

namespace Game.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        public void Load(Scene scene, Action onLoaded = null);
    }
}