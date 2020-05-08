using UnityEngine;

namespace FPSLogic
{
    public sealed class EnemyIncreasingArmor : BaseEnemy
    {
        #region Fields

        [SerializeField] private float _armor = 0.0f;
        [SerializeField] private float _armorIncreaseAmount = 0.1f;
        [SerializeField] private float _chanceToIncreaseArmorAfterHit = 16.0f;

        private System.Random _chance = new System.Random();

        #endregion


        #region Methods

        protected override float GetDamageToHurt(float initialDamage)
        {
            return base.GetDamageToHurt(initialDamage) / _armor;
        }

        private void IncreaseArmor()
        {
            if (_chanceToIncreaseArmorAfterHit >= _chance.Next(0, 101))
            {
                _armor += _armorIncreaseAmount;
            }
        }

        #endregion


        #region ICollision

        public override void CollisionEnter(InfoCollision info)
        {
            base.CollisionEnter(info);

            IncreaseArmor();
        }

        #endregion
    }
}
