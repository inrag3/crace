using System.Threading.Tasks;
using Game.Services.SceneLoader;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game.Infrastructure.AssetManagement
{
    public interface ISceneProvider
    {
        public Task<SceneInstance> LoadScene(Scene scene);
    }
}