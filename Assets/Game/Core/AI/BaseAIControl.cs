using System;
using Game.Services.Input;
using UnityEngine;

namespace PG
{
    public class BaseAIControl : MonoBehaviour, IInputService
    {
        public event Action<float> Acceleration;
        public event Action<float> Braking;
        public event Action<float> Steering;
        public event Action<bool> HandBrake;
        public event Action NextGear;
        public event Action PreviousGear;

        protected BaseAIConfig BaseAIConfig;
        protected float MaxSpeed => BaseAIConfig.MaxSpeed;
        protected float MinSpeed => BaseAIConfig.MinSpeed;
        protected float ReverseWaitTime => BaseAIConfig.ReverseWaitTime;
        protected float ReverseTime => BaseAIConfig.ReverseTime;
        protected float BetweenReverseTimeForReset => BaseAIConfig.BetweenReverseTimeForReset;

        protected float OffsetToTargetPoint => BaseAIConfig.OffsetToTargetPoint;
        protected float SpeedFactorToTargetPoint => BaseAIConfig.SpeedFactorToTargetPoint;
        protected float OffsetTurnPrediction => BaseAIConfig.OffsetTurnPrediction;
        protected float SpeedFactorToTurnPrediction => BaseAIConfig.SpeedFactorToTurnPrediction;
        protected float LookAngleSppedFactor => BaseAIConfig.LookAngleSppedFactor;
        protected float SetSteerAngleMultiplayer => BaseAIConfig.SetSteerAngleMultiplayer;

        private float _acceleration { get; set; }
        public float _brakeReverse { get; protected set; }
        public float _horizontal { get; protected set; }
        
        public float _currentSpeed;


        public void SetAcceleration(float value)
        {
            _acceleration = value;
            Acceleration?.Invoke(_acceleration);
        }

        public void SetBrake(float value)
        {
            Braking?.Invoke(value);
            _brakeReverse = value;
        }

        public void SetHorizontal(float value)
        {
            _horizontal = value;
            Steering?.Invoke(value);
        }
        
        protected float Vertical
        {
            get => _acceleration + _brakeReverse;
            set
            {
                SetAcceleration(Mathf.Max(0, value));
                SetBrake(Mathf.Max(0, -value));
            }
        }

        public virtual void Start()
        {
        }

        protected virtual void FixedUpdate()
        {
        }
    }

    [Serializable]
    public class BaseAIConfig
    {
        public float MaxSpeed = 80; 
        public float MinSpeed = 6; 
        public float SetSteerAngleMultiplayer = 2f; 

        public float OffsetToTargetPoint = 5;

        public float
            SpeedFactorToTargetPoint =
                0.5f; 

        public float OffsetTurnPrediction = 11; 

        public float
            SpeedFactorToTurnPrediction =
                0.6f; 

        public float
            LookAngleSppedFactor =
                30f; 

        public float
            ReverseWaitTime = 2;

        public float ReverseTime = 2; 
        public float BetweenReverseTimeForReset = 6;
    }
}