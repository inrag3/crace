using System.Collections.Generic;
using Game.Core.Deform.Deformation.Damager;
using UnityEngine;

namespace Game.Core.Deform.Deformation.Repairer
{
    public class Repairer : MonoBehaviour, IChanger
    {
        private IEnumerable<IDeformable> _deformables;

        public void Init(IEnumerable<IDeformable> deformables)
        {
            _deformables = deformables;
        }

        public void Change()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(IEnumerable<IDeformable> value)
        {
            throw new System.NotImplementedException();
        }
    }
}