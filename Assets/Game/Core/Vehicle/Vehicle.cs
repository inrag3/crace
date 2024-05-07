using System.Collections.Generic;
using Game.Core.Configs;
using Game.Core.Engines;
using Game.Core.Gearboxes;
using Game.Core.Steerings;
using Game.Core.Wheels;
using Game.Infrastructure.Factories.Engines;
using Game.Infrastructure.Factories.Gearboxes;
using UnityEngine;
using Zenject;

namespace Game.Core.Vehicle
{
    [RequireComponent(typeof(Rigidbody))]
    public class Vehicle : MonoBehaviour, IVehicle, IInitializable
    {
        [SerializeField] private EngineConfig _engineConfig;
        [SerializeField] private GearboxConfig _gearboxConfig;
        [SerializeField] private SteeringConfig _steeringConfig;

        private Engine _engine;
        private IGearbox _gearbox;
        private ITransmission _transmission;

        private IEngineFactory _engineFactory;
        private IGearboxFactory _gearboxFactory;

        private IWheel[] _wheels;
        private IList<IFixedTickable> _fixedTickables;
        private Steering _steering;
        private Rigidbody _rigidbody;

        [Inject]
        private void Construct(IEngineFactory engineFactory, IGearboxFactory gearboxFactory)
        {
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


            _gearbox = _gearboxFactory.Create(_gearboxConfig);
            _engine = _engineFactory.Create(_wheels, _gearbox, _engineConfig);
            _engine = _engineFactory.Create(_wheels, _gearbox, _engineConfig);
            _transmission = new Transmission(_wheels, _gearbox, _engine);
            _steering = new Steering();


            _fixedTickables.Add(_engine);
            _fixedTickables.Add(_steering);
            _fixedTickables.Add(_transmission);

            _engine.Start();
        }

        private void FixedUpdate()
        {
            foreach (var tickable in _fixedTickables)
            {
                tickable?.FixedTick();
            }
        }

        public void Brake(float value) => _transmission.Brake(value);

        public void Accelerate(float value) => _engine.SetAcceleration(value);
        public void Steering(float value)
        {
            throw new System.NotImplementedException();
        }

        public void NextGear() => _gearbox.Next();

        public void PreviousGear() => _gearbox.Previous();


        public void StopEngine() => _engine.Stop();
    }
}
