using System.Collections.Generic;
using Game.Core.VehicleSystem.Engines;
using Game.Core.VehicleSystem.Gearboxes;
using Game.Core.VehicleSystem.Wheels;
using UnityEngine;
using Zenject;

namespace Game.Core.VehicleSystem.Transmission
{
    public class Transmission : ITransmission
    {
        private readonly IReadOnlyList<Wheel> _wheels;
        private readonly Gearbox _gearbox;
        private readonly Engine _engine;

        public Transmission(IReadOnlyList<Wheel> wheels, Gearbox gearbox, Engine engine)
        {
            _engine = engine;
            _wheels = wheels;
            _gearbox = gearbox;
        }

        private bool InChangeGear => _gearbox.IsChanging;
        private int CurrentGear => _gearbox.CurrentGear;

        public void FixedTick()
        {
            if (!Mathf.Approximately(_engine.Acceleration, 0))
            {
                var motorTorque = _engine.Acceleration *
                                  (_engine.Torque * (_engine.MaxTorque * _gearbox.GetRatio(CurrentGear)));

                if (InChangeGear)
                {
                    motorTorque = 0;
                }

                //Calculation of target rpm for driving wheels.
                var targetRPM = _gearbox.GetRatio(CurrentGear) == 0
                    ? 0
                    : _engine.RevolutionsPerMinute / _gearbox.GetRatio(CurrentGear);
                var offset = (400 / Mathf.Abs(_gearbox.GetRatio(CurrentGear)));

                for (int i = 0; i < _wheels.Count; i++)
                {
                    var wheel = _wheels[i];
                    var wheelTorque = motorTorque;
    
                    if (targetRPM != 0 && Mathf.Sign(targetRPM * wheel.RevolutionPerMinute) > 0)
                    {
                        var multiplier = Mathf.Abs(wheel.RevolutionPerMinute) / Mathf.Abs(targetRPM) + offset;
                        if (multiplier >= 1f)
                        {
                            wheelTorque *= (1 - multiplier);
                        }
                    }

                    wheel.SetMotorTorque(wheelTorque);
                }
            }
            else
            {
                for (int i = 0; i < _wheels.Count; i++)
                {
                    _wheels[i].SetMotorTorque(0);
                }
            }


            if (_gearbox.IsChanging || !_gearbox.Config.IsAutomatic)
                return;


            //Automatic gearbox logic. 
            bool forwardIsSlip = false;
            bool anyWheelIsGrounded = false;
            float avgSign = 0;
            for (int i = 0; i < _wheels.Count; i++)
            {
                forwardIsSlip |= _wheels[i].ForwardSlipNormalized > 0.9f;
                anyWheelIsGrounded |= _wheels[i].IsGrounded;
                avgSign += _wheels[i].RevolutionPerMinute;
            }

            avgSign = Mathf.Sign(avgSign);


            if (anyWheelIsGrounded && !forwardIsSlip && _engine.RevolutionsPerMinute > _engine.Config.RPMToNextGear &&
                CurrentGear >= 0 &&
                CurrentGear < (_gearbox.Count - 2))
            {
                NextGear();
            }
            else if (CurrentGear > 0 && (_engine.RevolutionsPerMinute + 10 <= _engine.Config.MaxTorque || CurrentGear != 1) &&
                     _engine.Config.RPMToNextGear >
                     _engine.RevolutionsPerMinute / _gearbox.GetRatio(CurrentGear) *_gearbox.GetRatio(CurrentGear - 1) +
                     _engine.Config.RPMToPrevGearDiff)
            {
                PrevGear();
            }

            //Switching logic from neutral gear.
            if (CurrentGear == 0)
            {
                PrevGear();
            }
            else if (CurrentGear == 0 && _engine.Acceleration > 0)
            {
                NextGear();
            }
            else if ((avgSign > 0 && CurrentGear < 0) &&
                     Mathf.Approximately(_engine.Acceleration, 0))
            {
                _gearbox.Neutral();
            }
        }

        public void Brake(float value)
        {
            _wheels.Apply(x => x.Brake(value));
        }

        public void NextGear()
        {
            if (InChangeGear || CurrentGear >= _gearbox.Count - 2)
                return;
            _gearbox.Next();
        }

        public void PrevGear()
        {
            if (InChangeGear || CurrentGear < 0)
                return;
            _gearbox.Previous();
        }
    }

    public interface ITransmission : IFixedTickable
    {
        public void Brake(float value);
    }
}