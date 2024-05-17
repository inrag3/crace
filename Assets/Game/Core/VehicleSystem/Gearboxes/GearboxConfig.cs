using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.VehicleSystem.Gearboxes
{
    [CreateAssetMenu(fileName = "GearboxConfig", menuName = "Configs/GearboxConfig")]
    public class GearboxConfig : ScriptableObject
    {
        [SerializeField] private float[] _ratio;
        [field: SerializeField] public float MainRatio { get; private set; }
        [field: SerializeField] public float ChangingGearDelay { get; private set; } = 0.3f;
        [field: SerializeField] public bool IsAutomatic { get; private set; } = true;
        public IReadOnlyList<float> Ratio => _ratio;
    }
}