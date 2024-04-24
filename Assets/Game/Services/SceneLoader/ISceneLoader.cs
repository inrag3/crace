using System;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game.Services.SceneLoader
{
    public interface ISceneLoader
    {
        public Task<SceneInstance> Load(Scene scene, Action onLoaded = null);
    }
}