using System;
using UnityEngine;

namespace Game.Core.Configs
{
    [CreateAssetMenu(fileName = "EngineConfig", menuName = "Configs/EngineConfig")]
    public class EngineConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxTorque { get; private set; }
        [field: SerializeField] public AnimationCurve TorqueCurve { get; private set; }
        [field: SerializeField] public Range RevolutionsPerMinute { get; private set; }
        [field: SerializeField, Min(0)] public float Delay { get; private set; }

        [field: SerializeField] public float RPMEngineToRPMWheelsFast { get; private set; }
        [field: SerializeField] public float RPMEngineToRPMWheelsSlow { get; private set; }
    
        [field: SerializeField] public float CutOffRPM { get; private set; }
        [field: SerializeField] public float TargetCutOffRPM { get; private set; } 
        [field: SerializeField] public float CutOffTime { get; private set; }
    
        [field: SerializeField] public float RPMToNextGear { get; private set; }
        [field: SerializeField] public float RPMToPrevGearDiff { get; private set; }
    }
    
    [Serializable]
    public class Range
    {
        [field: SerializeField, Min(0)] public float Min { get; private set; }
        [field: SerializeField, Min(0)] public float Max { get; private set; }
    }
}