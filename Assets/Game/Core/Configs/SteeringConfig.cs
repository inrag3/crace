using UnityEngine;

namespace Game.Core.Configs
{
    [CreateAssetMenu(fileName = "SteeringConfig", menuName = "Configs/SteeringConfig")]
    public class SteeringConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxSteerAngle { get; private set; }
        [field: SerializeField] public AnimationCurve SteerLimitCurve { get; private set; }
        [field: SerializeField] public bool EnableSteerLimit { get; private set; } = true;
        [field: SerializeField] public AnimationCurve SteerChangeSpeedToVelocity { get; private set; }

        [field: SerializeField] public AnimationCurve SteerChangeSpeedFromVelocity { get; private set; }
        [field: SerializeField] public float MaxVelocityAngleForHelp { get; private set; } = 120;

        [field: SerializeField] public float MinSpeedForHelp { get; private set; } = 1.5f;

        [field: Space(10)]
        [field: SerializeField, Range(0, 1)] public float HelpDriftIntensity { get; private set; } = 0.8f;

        [field: SerializeField] public AnimationCurve HandBrakeAngularHelpCurve { get; private set; }

        [field: SerializeField] public AnimationCurve DriftResistanceCurve { get; private set; }

        [field: SerializeField] public float MaxSpeedForMaxAngularHelp { get; private set; } = 20;

        [field: SerializeField] public float DriftLimitAngle { get; private set; } = 0;
    
        [field: SerializeField, Range(0, 1)] public float ABS { get; private set; }
    
        [field: SerializeField, Range(0, 1)] public float TCS { get; private set; }
    }
}