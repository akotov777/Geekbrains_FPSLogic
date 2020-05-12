using UnityEngine;


namespace FPSLogic
{
    public sealed class EnemyStaticArmor : BaseEnemy
    {
        #region Fields

        [SerializeField] private float _armor = 1.5f;

        #endregion


        #region Methods

        protected override float GetDamageToHurt(float initialDamage)
        {
            return base.GetDamageToHurt(initialDamage) / _armor;
        }

        #endregion
    }
}
