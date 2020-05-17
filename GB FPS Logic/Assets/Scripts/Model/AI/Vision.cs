using UnityEngine;


namespace FPSLogic
{
    [System.Serializable]
    public sealed class Vision
    {
        #region Fields

        [SerializeField] private float _activeDistance = 10;
        [SerializeField] private float _activeAngle = 70;

        #endregion


        #region Methods

        private bool IsInDistance(Transform origin, Transform target)
        {
            return (origin.position - target.position).sqrMagnitude <= _activeDistance * _activeDistance;
        }

        private bool IsInAngle(Transform origin, Transform target)
        {
            var angle = Vector3.Angle(target.position - origin.position, origin.forward);
            return angle <= _activeAngle * 0.5;
        }

        private bool IsBlocked(Transform origin, Transform target)
        {
            if (!Physics.Linecast(origin.position, target.position, out var hit)) return true;
            return hit.transform != target;
        }

        public bool IsInVision(Transform origin, Transform target)
        {
            return IsInDistance(origin, target) && IsInAngle(origin, target) && !IsBlocked(origin, target);
        }

        #endregion
    }
}