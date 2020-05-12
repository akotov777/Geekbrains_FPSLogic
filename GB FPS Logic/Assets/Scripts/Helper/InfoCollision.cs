using UnityEngine;


namespace FPSLogic
{
    public readonly struct InfoCollision
    {
        #region Fields

        private readonly Vector3 _dir;
        private readonly float _damage;

        #endregion


        #region Properties

        public Vector3 Dir => _dir;

        public float Damage => _damage;

        #endregion


        #region ClassLifeCycles

        public InfoCollision(float damage, Vector3 dir = default)
        {
            _damage = damage;
            _dir = dir;
        }

        #endregion

    }
}
