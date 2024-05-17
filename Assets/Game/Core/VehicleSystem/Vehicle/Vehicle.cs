using System.Collections.Generic;
using Game.Core.VehicleSystem.Configs;
using Game.Core.VehicleSystem.Gearboxes;
using Game.Core.VehicleSystem.Steerings;
using Game.Core.VehicleSystem.Transmission;
using Game.Core.VehicleSystem.Wheels;
using Game.Infrastructure.Factories.Engines;
using Game.Infrastructure.Factories.Gearboxes;
using UnityEngine;
using Zenject;
using Engine = Game.Core.VehicleSystem.Engines.Engine;
using GearboxConfig = Game.Core.VehicleSystem.Configs.GearboxConfig;

namespace Game.Core.VehicleSystem.Vehicle
{
    [RequireComponent(typeof(Rigidbody))]
    public class Vehicle : MonoBehaviour, IVehicle, IInitializable
    {
        [field: SerializeField] public SteeringConfig SteeringConfig { get; private set; }
        [field: SerializeField] public EngineConfig EngineConfig { get; private set; }
        [field: SerializeField] public GearboxConfig GearboxConfig { get; private set; }
        public float VelocityAngle { get; set; }
        public float CurrentSpeed { get; set; }

        private Engine _engine;
        private IGearbox _gearbox;
        private ITransmission _transmission;

        private IEngineFactory _engineFactory;
        private IGearboxFactory _gearboxFactory;

        private IWheel[] _wheels;
        private IList<IFixedTickable> _fixedTickables;
        private ISteering _steering;
        private Rigidbody _rigidbody;

        [Inject]
        private void Construct(IEngineFactory engineFactory, IGearboxFactory gearboxFactory, ITransmission transmission, ISteering steering)
        {
            _steering = steering;
            _transmission = transmission;
            _gearboxFactory = gearboxFactory;
            _engineFactory = engineFactory;
            _fixedTickables = new List<IFixedTickable>();
        }

        public void Initialize() => StartEngine();
        public void StartEngine() => _engine.Start();


        private void Awake()
        {
            _wheels = GetComponentsInChildren<IWheel>();
            _rigidbody = GetComponent<Rigidbody>();
            _gearbox = _gearboxFactory.Create(GearboxConfig);
            _engine = _engineFactory.Create(_wheels, _gearbox, EngineConfig);
            _fixedTickables.Add(_engine);
            _fixedTickables.Add(_steering);
            _fixedTickables.Add(_transmission);
            _engine.Start();
        }

        private void FixedUpdate()
        {
            CurrentSpeed = _rigidbody.velocity.magnitude;
            foreach (var tickable in _fixedTickables)
            {
                tickable?.FixedTick();
            }
        }

        public void Brake(float value) => _transmission.Brake(value);
        public void Accelerate(float value) => _engine.SetAcceleration(value);
        public void Steering(float value) => _steering.SetSteer(value);
        public void NextGear() => _gearbox.Next();
        public void PreviousGear() => _gearbox.Previous();
        public void HandBrake(bool value) => _transmission.Brake(5000);
        public void StopEngine() => _engine.Stop();
    }
}
