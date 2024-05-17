using System.Collections.Generic;
using Game.Core.VehicleSystem.Configs;
using Game.Core.VehicleSystem.Engines;
using Game.Core.VehicleSystem.Gearboxes;
using Game.Core.VehicleSystem.Wheels;
using UnityEngine;

namespace Game.Core.VehicleSystem.Steerings
{
    public class Steering : ISteering
    {
        private readonly SteeringConfig _config;
        private readonly Engine _engine;
        private readonly IReadOnlyList<Wheel> _wheels;
        private readonly Rigidbody _rigidbody;
        public Steering(IReadOnlyList<Wheel> wheels, SteeringConfig config, Engine engine, Rigidbody rigidbody,
            Vehicle.Vehicle vehicle, Gearbox gearbox)
        {
            _gearbox = gearbox;
            _vehicle = vehicle;
            _rigidbody = rigidbody;
            _wheels = wheels;
            _engine = engine;
            _config = config;
        }

        protected float PrevSteerAngle;
        protected float CurrentSteerAngle;
        protected float WheelMaxSteerAngle;
        protected Wheel[] SteeringWheels;
        private Vehicle.Vehicle _vehicle;
        private Gearbox _gearbox;
        
        public void Initialize()
        {
            var steeringWheels = new List<Wheel>();
            foreach (var wheel in _wheels)
            {
                if (!wheel.IsSteeringWheel) 
                    continue;
                steeringWheels.Add(wheel);
                var percent = Mathf.Abs(wheel.SteerPercent);
                if (percent > WheelMaxSteerAngle)
                {
                    WheelMaxSteerAngle = percent;
                }
            }
            SteeringWheels = steeringWheels.ToArray();
        }
        
        public void FixedTick()
        {
            var needHelp = _vehicle.VelocityAngle > 0.001f &&
                           _vehicle.VelocityAngle < _config.MaxVelocityAngleForHelp &&
                           _vehicle.CurrentSpeed > _config.MinSpeedForHelp && _gearbox.CurrentGear > 0;
            float helpAngle = 0;
            var angularVelocity = _rigidbody.angularVelocity;

            if (needHelp)
            {
                helpAngle = Mathf.Clamp(_vehicle.VelocityAngle * _config.HelpDriftIntensity, -_config.MaxSteerAngle,
                    _config.MaxSteerAngle);
            }
            else if (_vehicle.CurrentSpeed < _config.MinSpeedForHelp && _engine.Acceleration > 0)
            {
                angularVelocity.y += _config.HandBrakeAngularHelpCurve.Evaluate(angularVelocity.y) *
                                     5 * Time.fixedDeltaTime;
                _rigidbody.angularVelocity = angularVelocity;
            }
            
            var targetAngle = Mathf.Clamp(helpAngle, -_config.MaxSteerAngle, _config.MaxSteerAngle);
            
            float steerAngleChangeSpeed;

            float currentAngleDiff = Mathf.Abs(_vehicle.VelocityAngle - CurrentSteerAngle);

            if (!needHelp || PrevSteerAngle > CurrentSteerAngle && CurrentSteerAngle > _vehicle.VelocityAngle ||
                PrevSteerAngle < CurrentSteerAngle && CurrentSteerAngle < _vehicle.VelocityAngle)
            {
                steerAngleChangeSpeed = _config.SteerChangeSpeedToVelocity.Evaluate(currentAngleDiff);
            }
            else
            {
                steerAngleChangeSpeed = _config.SteerChangeSpeedFromVelocity.Evaluate(currentAngleDiff);
            }

            PrevSteerAngle = CurrentSteerAngle;
            CurrentSteerAngle = Mathf.MoveTowards(CurrentSteerAngle, targetAngle,
                steerAngleChangeSpeed * Time.fixedDeltaTime);
            CurrentSteerAngle = Mathf.Clamp(CurrentSteerAngle,  -_config.MaxSteerAngle, _config.MaxSteerAngle);

            //Apply a turn to the front wheels.
            for (int i = 0; i < SteeringWheels.Length; i++)
            {
                SteeringWheels[i].SetSteerAngle(CurrentSteerAngle);
            }
        }

        public float Steer { get; }
        public void SetSteer(float value)
        {
            throw new System.NotImplementedException();
        }
    }
}