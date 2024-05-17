using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.VehicleSystem.Wheels
{
    /// <summary>
    /// Wrapper for WheelCollider. The current slip, temperature, surface, etc. is calculated.
    /// </summary>
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour, ISuspendable, IWheel
    {
        [Range(-1f, 1f)] public float
            SteerPercent; //Percentage of wheel turns, 1 - maximum possible turn CarController.Steer.MaxSteerAngle, -1 negative wheel turn (For example, to turn the rear wheels).

        public float MaxBrakeTorque;
        public bool HandBrakeWheel;
        [Tooltip("The angle at which the wheel leans at the extreme points of the suspension (Only visual effect)")]
        public float MaxSuspensionWheelAngle;

        public float RevolutionPerMinute => Collider.rpm;

        public float CurrentForwardSlip { get; private set; }
        public float ForwardSlipNormalized { get; private set; }
        public float SuspensionPos => _suspension.Position;
        public WheelHit GetHit => Hit;

        public float RotationPerMinute { get; }
        public bool IsGrounded => Collider.isGrounded;
        public bool IsDrive { get; }
        public void Brake(float value)
        {
            throw new System.NotImplementedException();
        }

        public void MotorTorque(float value)
        {
            throw new System.NotImplementedException();
        }

        public Suspension Suspension { get; }
        public Vector3 LocalPositionOnAwake { get; private set; }

        public bool IsSteeringWheel => !Mathf.Approximately(0, SteerPercent);
        
        Dictionary<Transform, Quaternion> InitialChildRotations = new Dictionary<Transform, Quaternion>();
        public WheelCollider Collider { get; private set; }

        [System.NonSerialized] private Vector3 Position;

        public Vector3 LocalPosition { get; private set; }
        
        public WheelHit Hit;

    
        float CurrentBrakeTorque;
        
        private readonly WheelView _wheelView;
        private Suspension _suspension;
        public float SuspensionDistance => Collider.suspensionDistance;
        public Vector3 Center => Collider.center;
        public float MaxSuspensionAngle => MaxSuspensionWheelAngle;

        public void Awake()
        {
            Collider = GetComponent<WheelCollider>();
            Collider.ConfigureVehicleSubsteps(40, 100, 20);

            LocalPositionOnAwake = transform.localPosition;

            Collider.GetWorldPose(out Position, out _);

            LocalPosition = Position - transform.position;


            _suspension = new Suspension(this);

            _suspension.Calculate(LocalPosition.y);
        }
        
        public void FixedUpdate()
        {
            Collider.GetWorldPose(out Position, out _);
            LocalPosition = transform.InverseTransformPoint(Position);

            if (Collider.GetGroundHit(out Hit))
            {
                CurrentForwardSlip = (CurrentForwardSlip + Mathf.Abs(Hit.forwardSlip)) / 2;

                ForwardSlipNormalized = CurrentForwardSlip / Collider.forwardFriction.extremumSlip;

                _suspension.Calculate(LocalPosition.y);
            }

            ApplyBrake();
        }


        private void ApplyBrake()
        {
            if (CurrentBrakeTorque > Collider.brakeTorque)
            {
                Collider.brakeTorque = Mathf.Lerp(Collider.brakeTorque, CurrentBrakeTorque, 2 * Time.fixedDeltaTime);
            }
            else
            {
                Collider.brakeTorque = CurrentBrakeTorque;
            }
        }
        
        public void SetMotorTorque(float motorTorque)
        {
            Collider.motorTorque = motorTorque;
        }

        public void SetSteerAngle(float steerAngle)
        {
            Collider.steerAngle = steerAngle * SteerPercent;
        }

        public void SetHandBrake()
        {
            if (HandBrakeWheel)
            {
                CurrentBrakeTorque = MaxBrakeTorque;
            }
            else
            {
                CurrentBrakeTorque = 0;
            }
        }
        public void SetBrakeTorque(float brakeTorque)
        {
            CurrentBrakeTorque = brakeTorque * MaxBrakeTorque;
        }
    }

    public interface ISuspendable
    {
        public float SuspensionDistance { get; }
        public Vector3 Center { get; }
        public float MaxSuspensionAngle { get; }
    }
}
