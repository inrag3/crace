using UnityEngine;

namespace Game.Core.VehicleSystem.Wheels
{
    [RequireComponent(typeof(Wheel))]
    public class WheelView : MonoBehaviour
    {
        [field: SerializeField] public Transform View { get; private set; }

        private Wheel _wheel;
        private float _angle;

        private void Awake()
        {
            _wheel = GetComponent<Wheel>();
        }

        private void LateUpdate()
        {
            var angle = _wheel.Collider.steerAngle;
            View.localPosition = Vector3.Lerp(View.localPosition, _wheel.LocalPosition, 0.25f);

            _angle += _wheel.RevolutionPerMinute * Mathf.PI * Time.deltaTime;
            View.localRotation = Quaternion.AngleAxis(angle, Vector3.forward) *
                                 Quaternion.AngleAxis(_wheel.Collider.steerAngle, Vector3.up) *
                                 Quaternion.AngleAxis(_angle, Vector3.right);
        }
    }
}

