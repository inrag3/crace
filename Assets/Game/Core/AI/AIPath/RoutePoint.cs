using System;
using UnityEngine;

namespace Game.Core.AI.AIPath
{
    public partial class Path
    {
        public struct RoutePoint
        {
            public Vector3 Position;
            public Vector3 Direction;

            public float SpeedLimit;

            public RoutePoint(Vector3 position, Vector3 direction,
                float speedLimit)
            {
                Position = position;
                Direction = direction;
                SpeedLimit = speedLimit;
            }
        }

        [Serializable]
        public struct WaypointData
        {
            public Transform Point;
            public float OvertakeZoneLeft;
            public float OvertakeZoneRight;
            public float SpeedLimit;
            public Vector3 Position => Point ? Point.position : Vector3.zero;

            public WaypointData(Transform point, float overtakeZoneLeft = 2, float overtakeZoneRight = 2,
                float speedLimit = float.PositiveInfinity)
            {
                Point = point;
                OvertakeZoneLeft = overtakeZoneLeft;
                OvertakeZoneRight = overtakeZoneRight;
                SpeedLimit = speedLimit;
            }
        }
    }
}