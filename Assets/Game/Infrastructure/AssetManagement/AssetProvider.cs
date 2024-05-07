using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Services.Logger;
using Game.Services.SceneLoader;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

namespace Game.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider, IInitializable, IDisposable
    {
        private readonly IDictionary<string, AsyncOperationHandle> _cache =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly IDictionary<string, List<AsyncOperationHandle>> _handles =
            new Dictionary<string, List<AsyncOperationHandle>>();

        public async Task<T> Load<T>(string key) where T : class
        {
            if (_cache.TryGetValue(key, out var completedHandle))
            {
                return completedHandle.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(key);
            handle.Completed += asyncOperationHandle => { _cache[key] = asyncOperationHandle; };

            if (!_handles.TryGetValue(key, out var handles))
            {
                handles = new List<AsyncOperationHandle>();
                _handles[key] = handles;
            }

            handles.Add(handle);

            return await handle.Task;
        }

        public void Initialize() => 
            Addressables.InitializeAsync();

        public void Release(string key)
        {
            if (!_handles.ContainsKey(key))
                return;

            foreach (var handle in _handles[key])
                Addressables.Release(handle);

            _cache.Remove(key);
            _handles.Remove(key);
        }

        public async Task<SceneInstance> LoadScene(Scene name)
        {
            var handle = Addressables.LoadSceneAsync(name.ToString());
            return await handle.Task;
        }

        public void Dispose() => Release();

        private void Release()
        {
            foreach (var list in _handles.Values)
            {
                foreach (var handle in list)
                {
                    Addressables.Release(handle);
                }
            }

            _cache.Clear();
            _handles.Clear();
        }
    }
}
