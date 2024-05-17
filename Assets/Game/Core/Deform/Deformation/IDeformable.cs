using System;
using UnityEngine;

namespace Game.Core.Deform.Deformation
{
    public interface IDeformable
    {
        public event Action<Collision> Entered;
        public MeshVertices InitialVertices { get; }
        public MeshFilter Filter { get; }
    }
}