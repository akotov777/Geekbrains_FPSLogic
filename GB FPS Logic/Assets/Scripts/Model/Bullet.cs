using UnityEngine;


namespace FPSLogic
{
    public sealed class Bullet : Ammunition
    {
        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            if (collision is IPenetrable)
            {
                var damageReduce = collision.
                                   gameObject.GetComponent<IPenetrable>().
                                   GetDamageReduce();
                LossOfDamageByPenetrating(damageReduce);
                DestroyIfNegativeDamage();
            }

            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_currentDamage, Rigidbody.velocity));
            }

            DestroyAmmunition();
        }

        #endregion
    }
}
