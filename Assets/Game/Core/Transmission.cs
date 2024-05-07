using System.Collections.Generic;
using System.Linq;
using Game.Core.Engines;
using Game.Core.Gearboxes;
using Game.Core.Wheels;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public class Transmission : ITransmission
    {
        private const float MINIMUM_ENGINE_TORQUE = 0;

        private IReadOnlyList<IWheel> _wheels;
        private IGearbox _gearbox;
        private IEngine _engine;

        public Transmission(IReadOnlyList<IWheel> wheels, IGearbox gearbox, IEngine engine)
        {
            _engine = engine;
            _wheels = wheels;
            _gearbox = gearbox;
        }

        public void FixedTick()
        {
            var motorTorque = MINIMUM_ENGINE_TORQUE;

            if (_engine.Acceleration > 0)
            {
                var ratio = _gearbox.GetRatio();
                motorTorque = _engine.Acceleration * _engine.Torque * 
                              _engine.MaxTorque * ratio;
            }
            _wheels.Apply(wheel => wheel.MotorTorque(motorTorque));
        }

        public void Brake(float value)
        {
            _wheels.Apply(wheel => wheel.Brake(value));
        }
    }


    public interface ITransmission : IFixedTickable
    {
        public void Brake(float value);
    }
}

