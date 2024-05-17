using Game.Core.AI.AIPath;
using PG;
using UnityEngine;

namespace Game.Core.AI
{
    public class PositioningAIControl : BaseAIControl
    {
        public Path path; //The path to which the AI is tied.

        protected float SpeedLimit;
        protected float ProgressDistance { get; set; } //Distance of progress along the AIPath
        private Path.RoutePoint ProgressPoint { get; set; }

        public override void Start()
        {
            base.Start();
            ProgressPoint = path.GetRoutePoint(0);

            float minProgress = 0;
            float curProgress = 0;
            var minDist = (path.GetRoutePoint(0).Position - transform.position).sqrMagnitude;
            while (curProgress < path.Length)
            {
                curProgress += 0.5f;
                var curDist = (path.GetRoutePoint(curProgress).Position - transform.position).sqrMagnitude;
                if (!(curDist < minDist))
                    continue;
                minDist = curDist;
                minProgress = curProgress;
            }

            ProgressDistance = minProgress;
            ProgressPoint = path.GetRoutePoint(ProgressDistance);
        }

        protected override void FixedUpdate()
        {
            var progressDelta = ProgressPoint.Position - transform.position;
            var dotProgressDelta = Vector3.Dot(progressDelta, ProgressPoint.Direction);

            if (dotProgressDelta < 0)
            {
                while (dotProgressDelta < 0)
                {
                    ProgressDistance += Mathf.Max(0.5f, _currentSpeed * Time.fixedDeltaTime);
                    ProgressPoint = path.GetRoutePoint(ProgressDistance);
                    progressDelta = ProgressPoint.Position - transform.position;
                    dotProgressDelta = Vector3.Dot(progressDelta, ProgressPoint.Direction);
                }
            }
            else if (ProgressDistance > 0 && progressDelta.sqrMagnitude < 0)
            {
                dotProgressDelta = Vector3.Dot(progressDelta, -ProgressPoint.Direction);

                if (!(dotProgressDelta < 0f))
                    return;
                ProgressDistance -= progressDelta.magnitude * 0.5f;
                ProgressPoint = path.GetRoutePoint(ProgressDistance);
            }
        }
    }
}