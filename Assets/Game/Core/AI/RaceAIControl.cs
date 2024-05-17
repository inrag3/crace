using Game.Core.AI.AIPath;
using UnityEngine;

namespace Game.Core.AI
{
    public class RaceAIControl : PositioningAIControl
    {
        private bool _reverse;
        private float _reverseTimer = 0;
        private const float PrevSpeed = 0;
        private float _lastReverseTime;
        private float CurrentHorizontalOffset { get; set; }
        private Path.RoutePoint TargetPoint { get; set; }
        private Path.RoutePoint TurnPredictionPoint { get; set; }

        private Vector3 _horizontalOffset;
        private Rigidbody _aheadRb;
        private float _distanceToAheadCollider;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_reverse)
            {
                ReverseMove();
            }
            else
            {
                ForwardMove();
            }
        }

        private void ForwardMove()
        {
            TargetPoint = path.GetRoutePoint(ProgressDistance + OffsetToTargetPoint +
                                             (SpeedFactorToTargetPoint * _currentSpeed));
            TurnPredictionPoint = path.GetRoutePoint(ProgressDistance + OffsetTurnPrediction +
                                                     (SpeedFactorToTurnPrediction * _currentSpeed));
            var angleToPredictionPoint = Mathf.Abs(Vector3.SignedAngle(Vector3.forward,
                transform.InverseTransformPoint((TurnPredictionPoint.Position)),
                transform.up));

            var desiredSpeed = (1 - (angleToPredictionPoint / LookAngleSppedFactor));
            desiredSpeed = desiredSpeed * (SpeedLimit - MinSpeed) + MinSpeed;
            desiredSpeed = Mathf.Clamp(desiredSpeed, MinSpeed, MaxSpeed);

            //Apply speed limit.
            desiredSpeed = Mathf.Min(SpeedLimit, desiredSpeed);

            // Acceleration and brake logic
            Vertical = Mathf.Clamp(((desiredSpeed / _currentSpeed - 1)), -1, 1);

            _horizontalOffset =
                Vector3.Cross(TargetPoint.Direction.normalized, transform.up) * -CurrentHorizontalOffset;

            //Steer angle logic
            var angleToTargetPoint = Vector3.SignedAngle(Vector3.forward,
                transform.InverseTransformPoint((TargetPoint.Position + _horizontalOffset)),
                Vector3.up);
            SetHorizontal(Mathf.Clamp((angleToTargetPoint * SetSteerAngleMultiplayer),
                -1, 1));

            //Reverse logic
            var deltaSpeed = Mathf.Abs(_currentSpeed - PrevSpeed);
            if (Vertical > 0.1f && deltaSpeed < 1 && _currentSpeed < 10)
            {
                if (_reverseTimer < ReverseWaitTime)
                {
                    _reverseTimer += Time.fixedDeltaTime;
                }
                else if (Time.time - _lastReverseTime <= BetweenReverseTimeForReset)
                {
                    _horizontal = 0;
                    Vertical = 0;
                    _reverseTimer = 0;
                }
                else
                {
                    _horizontal = -_horizontal;
                    Vertical = -Vertical;
                    _reverseTimer = 0;
                    _reverse = true;
                }
            }
            else
            {
                _reverseTimer = 0;
            }
        }

        private void ReverseMove()
        {
            if (_reverseTimer < ReverseTime)
            {
                _reverseTimer += Time.fixedDeltaTime;
            }
            else
            {
                _lastReverseTime = Time.time;
                _reverseTimer = 0;
                _reverse = false;
            }
        }
    }
}