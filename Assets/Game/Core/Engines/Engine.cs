using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Configs;
using Game.Core.Gearboxes;
using Game.Core.Wheels;
using Game.Services.CoroutinePerformer;
using Game.Services.Logger;
using UnityEngine;

namespace Game.Core.Engines
{
    public class Engine : IEngine
    {
        private readonly IReadOnlyList<IWheel> _wheels;
        private readonly ICoroutinePerformer _performer;
        private readonly WaitForSeconds _waiter;
        private readonly EngineConfig _config;
        private Coroutine _coroutine;
        private ILoggerService _logger;
        private IGearbox _gearbox;

        public Engine(IReadOnlyList<IWheel> wheels, IGearbox gearbox, ICoroutinePerformer performer, EngineConfig config,
            ILoggerService logger)
        {
            _logger = logger;
            _config = config;
            _performer = performer;
            _gearbox = gearbox;
            _wheels = wheels;
            MaxTorque = _config.MaxTorque / _wheels.Count(x => x.IsDrive);
        }
        
        public bool IsOn { get; private set; }
        public float RevolutionsPerMinute { get; private set; } // Обороты в минуту
        public float Acceleration { get; private set; }
        public float MaxTorque { get; }
        public float Torque => _config.TorqueCurve.Evaluate(RevolutionsPerMinute * 0.001f);

        public void Start()
        {
            if (_coroutine != null || IsOn)
                return;
            _coroutine = _performer.StartPerform(Activate());
        }

        public void Stop()
        {
            if (_coroutine == null)
                return;
            IsOn = false;
            _performer.StopPerform(_coroutine);
        }

        public void FixedTick()
        {
            var averageRotationPerMinute = 0f;
            var count = _wheels.Count(x => x.IsDrive);
            for (var i = 0; i < count; i++)
            {
                averageRotationPerMinute += _wheels.ElementAt(i).RotationPerMinute;
            }
            
            averageRotationPerMinute /= count;
            
            var targetRevolutionsPerMinute = (averageRotationPerMinute * _gearbox.CurrentGear) <= 0
                ? RevolutionsPerMinute * Acceleration
                : RevolutionsPerMinute * Mathf.Abs( _gearbox.GetRatio());
            
            targetRevolutionsPerMinute = targetRevolutionsPerMinute.Clamp(_config.RevolutionsPerMinute.Min, _config.RevolutionsPerMinute.Max);
            RevolutionsPerMinute = Mathf.Lerp(RevolutionsPerMinute, targetRevolutionsPerMinute, Time.fixedDeltaTime);
        }

        public void SetAcceleration(float value)
        {
            if (value < 0)
                _logger.Error("acceleration cannot be negative", this);
            Debug.Log("set acceleration: " + value);
            Acceleration = value;
        }

        private IEnumerator Activate()
        {
            var timer = 0f;
            while (timer < _config.Delay)
            {
                timer += Time.deltaTime;
                yield return null;
                RevolutionsPerMinute = Mathf.Lerp(0, _config.RevolutionsPerMinute.Min,
                    Mathf.Pow(timer / _config.Delay, 2));
            }
            IsOn = true;
            RevolutionsPerMinute = _config.RevolutionsPerMinute.Min;
            _coroutine = null;
        }
    }
}