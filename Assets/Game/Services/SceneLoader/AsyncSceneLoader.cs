using System;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using ISceneProvider = Game.Infrastructure.AssetManagement.ISceneProvider;

namespace Game.Services.SceneLoader
{
    public class AsyncSceneLoader : ISceneLoader
    {
        private readonly ISceneProvider _sceneProvider;
        
        public AsyncSceneLoader(ISceneProvider sceneProvider)
        {
            _sceneProvider = sceneProvider;
        }

        public async Task<SceneInstance> Load(Scene name, Action onLoaded = null)
        {
           var scene = await _sceneProvider.LoadScene(name);
           scene.ActivateAsync();
           onLoaded?.Invoke();
           return scene;
        }
    }
}