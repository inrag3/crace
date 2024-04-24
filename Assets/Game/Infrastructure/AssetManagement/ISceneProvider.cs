using System.Threading.Tasks;
using Game.Infrastructure.Services.SceneLoader;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game.Infrastructure.AssetManagement
{
    public interface ISceneProvider
    {
        public Task<SceneInstance> LoadScene(Scene scene);
    }
}