using System;
using UnityEngine;


namespace FPSLogic
{
    public abstract class BaseEnemyBodyPart : BaseSceneObject, ICollision
    {
        public event Action<InfoCollision> EnemyPartDamage;

        public virtual void CollisionEnter(InfoCollision info)
        {
            EnemyPartDamage?.Invoke(info);
        }
    }
}