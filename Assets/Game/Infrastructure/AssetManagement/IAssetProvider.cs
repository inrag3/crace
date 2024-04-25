using System.Threading.Tasks;

namespace Game.Infrastructure.AssetManagement
{
    public interface IAssetProvider : ISceneProvider
    {
        public Task<T> Load<T>(string key) where T : class;
        public void Release(string key);
        public void Release();
    }
}