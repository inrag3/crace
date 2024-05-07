using System.Threading.Tasks;
using Game.Core.Vehicle;
using Game.Infrastructure.AssetManagement;
using Game.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Factories.Vehicles
{
    public class VehicleFactory : IVehicleFactory
    {
        private const string VEHICLE = "Vehicle";
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _container;
        private Transform _parent;

        public VehicleFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }
        public async Task Prepare()
        {
            await _assetProvider.Load<GameObject>(key: VEHICLE);
        }
        
        public void CreateRoot() =>
            _parent = new GameObject("[Vehicles]").transform;
        
        public async Task<IVehicle> Create()
        {
            var prefab = await _assetProvider.Load<GameObject>(key: VEHICLE);
            return _container.InstantiatePrefabForComponent<IVehicle>(prefab, Vector3.up, Quaternion.identity, _parent);
        }

        public void Clear()
        {
            _assetProvider.Release(key: VEHICLE);
        }
        
    }
}