using UnityEngine;

namespace Game.Core.Deform.Deformation
{
    public struct MeshVertices
    {
        public Vector3[] Vertices;

        public MeshVertices(Vector3[] meshVertices)
        {
            Vertices = meshVertices;
        }
    }
}