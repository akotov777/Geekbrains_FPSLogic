using System;
using UnityEngine;


namespace FPSLogic
{
    public sealed class IncreasingArmorEnemyBodyPart : BaseEnemyBodyPart
    {
        #region Fields

        [SerializeField] private float _initialArmor = 0.0f;
        [SerializeField] private float _maxArmor = 2.0f;
        [SerializeField] private float _armorIncreaseAmount = 0.1f;
        [SerializeField] private float _chanceToIncreaseArmorAfterHit = 16.0f;

        private System.Random _chance = new System.Random();

        #endregion


        #region Methods

        private void IncreaseArmor()
        {
            if (_initialArmor >= _maxArmor) return;
            if (_chanceToIncreaseArmorAfterHit >= _chance.Next(0, 101))
            {
                _initialArmor += _armorIncreaseAmount;
            }
        }

        #endregion


        #region ICollision

        public override void CollisionEnter(InfoCollision info)
        {
            base.CollisionEnter(new InfoCollision(_initialArmor));

            IncreaseArmor();
        }

        #endregion
    }
}