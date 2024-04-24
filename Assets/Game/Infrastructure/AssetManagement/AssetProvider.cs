using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Infrastructure.Services.Logger;
using Game.Infrastructure.Services.SceneLoader;
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

        private readonly IList<AsyncOperationHandle> _handles =
            new List<AsyncOperationHandle>();

        private readonly ILoggerService _logger;

        public AssetProvider(ILoggerService logger)
        {
            _logger = logger;
        }
        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async Task<T> Load<T>(string key) where T : class
        {
            if (_cache.TryGetValue(key, out var completedHandle))
            {
                return completedHandle.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(key);
            handle.Completed += asyncOperationHandle =>
            {
                _cache[key] = asyncOperationHandle;
            };
            _handles.Add(handle);
            return await handle.Task;
        }

        public async Task<SceneInstance> LoadScene(Scene name)
        {
            var handle = Addressables.LoadSceneAsync(name.ToString());
            return await handle.Task;
        }
        public void Dispose() => Release();

        public void Release()
        {
            foreach (var handle in _handles)
            {
                Addressables.Release(handle);
            }
            _logger.Log("handles cleared", this);
        }
    }
}