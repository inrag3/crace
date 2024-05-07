using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Configs
{
    [CreateAssetMenu(fileName = "GearboxConfig", menuName = "Configs/GearboxConfig")]
    public class GearboxConfig : ScriptableObject
    {
        [SerializeField] public float[] _ratio;
        [field: SerializeField] public float MainRatio { get; private set; }
        [field: SerializeField] public float ChangingGearDelay { get; private set; } = 0.3f;
        public IReadOnlyList<float> Ratio => _ratio;
    }
}

[CreateAssetMenu(fileName = "GearboxConfig", menuName = "Configs/GearboxConfig")]
public class GearboxConfig : ScriptableObject
{
    [SerializeField] public float[] _ratio;
    [field: SerializeField] public float MainRatio { get; private set; }
    [field: SerializeField] public float ChangingGearDelay { get; private set; } = 0.3f;
    public IReadOnlyList<float> Ratio => _ratio;
}