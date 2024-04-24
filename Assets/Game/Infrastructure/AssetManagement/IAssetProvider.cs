using System.Threading.Tasks;
using Game.Infrastructure.Services.SceneLoader;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        public Task<SceneInstance> LoadScene(Scene scene);
        public Task<T> Load<T>(string key) where T : class;
        public void Release();
    }
}