using System;
using UnityEngine;

namespace Game.Core.Deform.Detector
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MeshCollider))]
    public class Detector : MonoBehaviour, IDetector<Collision>
    {
        public event Action<Collision> Detected;

        private void OnCollisionEnter(Collision collision)
        {
            Detected?.Invoke(collision);
        }
    }
}