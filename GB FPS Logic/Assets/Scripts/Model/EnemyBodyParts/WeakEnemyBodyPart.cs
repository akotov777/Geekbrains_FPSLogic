using UnityEngine;


namespace FPSLogic
{
    public sealed class WeakEnemyBodyPart : BaseEnemyBodyPart
    {
        #region Fields

        [SerializeField] private float _damageMultiplier = 5.0f;

        #endregion


        #region ICollision

        public override void CollisionEnter(InfoCollision info)
        {
            base.CollisionEnter(new InfoCollision(info.Damage * _damageMultiplier));
        }

        #endregion
    }
}