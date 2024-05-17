using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.AI.AIPath
{
    public partial class Path : MonoBehaviour
    {
        [field: SerializeField] public List<WaypointData> Waypoints = new();
        public float Length { get; private set; }
        private int _pointsCount;
        private List<Vector3> _points;
        private List<float> _distances;
        
        private int _p0;
        private int _p1;
        private int _p2;
        private int _p3;

        private float _inverse;
        private Vector3 _P0;
        private Vector3 _P1;
        private Vector3 _P2;
        private Vector3 _P3;

        private void Awake()
        {
            if (Waypoints.Count > 1)
            {
                CachePositionsAndDistances();
            }

            _pointsCount = Waypoints.Count;

            for (var i = 0; i < Waypoints.Count; i++)
            {
                var w = Waypoints[i];
                Waypoints[i] = w;
            }
        }

        public RoutePoint GetRoutePoint(float dist)
        {
            var p1 = GetRoutePosition(dist, out var overtakeZoneLeft, out var overtakeZoneRight, out var speedLimit);
            var p2 = GetRoutePosition(dist + 0.1f, out _, out _, out _);
            var delta = p2 - p1;
            return new RoutePoint(p1, delta.normalized, speedLimit);
        }

        private Vector3 GetRoutePosition(float dist, out float overtakeZoneLeft, out float overtakeZoneRight,
            out float speedLimit)
        {
            int point;

            dist = Mathf.Repeat(dist, Length);

            for (point = 0; point < _distances.Count; point++)
            {
                if (_distances[point] > dist)
                {
                    break;
                }
            }

            _p1 = ((point - 1) + _pointsCount) % _pointsCount;
            _p2 = point % _pointsCount;
            _inverse = Mathf.InverseLerp(_distances[_p1], _distances[point % _distances.Count], dist);
            overtakeZoneLeft = Mathf.Lerp(Waypoints[_p1].OvertakeZoneLeft, Waypoints[_p2].OvertakeZoneLeft, _inverse);
            overtakeZoneRight = Mathf.Lerp(Waypoints[_p1].OvertakeZoneRight, Waypoints[_p2].OvertakeZoneRight, _inverse);
            speedLimit = Mathf.Lerp(Waypoints[_p1].SpeedLimit, Waypoints[_p2].SpeedLimit, _inverse);
            
            _p0 = ((point - 2) + _pointsCount) % _pointsCount;
            _p3 = (point + 1) % _pointsCount;

            _p2 %= _pointsCount;

            _P0 = _points[_p0];
            _P1 = _points[_p1];
            _P2 = _points[_p2];
            _P3 = _points[_p3];

            return CatmullRom(_P0, _P1, _P2, _P3, _inverse);
        }

        private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float i)
        {
            return 0.5f *
                   ((2 * p1) + (-p0 + p2) * i + (2 * p0 - 5 * p1 + 4 * p2 - p3) * i * i +
                    (-p0 + 3 * p1 - 3 * p2 + p3) * i * i * i);
        }

        private void CachePositionsAndDistances()
        {
            _points = new List<Vector3>();
            _distances = new List<float>();

            float accumulateDistance = 0;
            for (var i = 0; i < Waypoints.Count; i++)
            {
                var t1 = Waypoints[(i) % Waypoints.Count];
                var t2 = Waypoints[(i + 1) % Waypoints.Count];

                var p1 = t1.Position;
                var p2 = t2.Position;
                _points.Add(Waypoints[i % Waypoints.Count].Position);
                _distances.Add(accumulateDistance);
                accumulateDistance += (p1 - p2).magnitude;
            }
            
            _distances.Add(accumulateDistance);
            _points.Add(Waypoints[0].Position);
            Length = _distances[^1];
        }
    }
}