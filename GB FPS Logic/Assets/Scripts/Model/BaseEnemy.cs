using UnityEngine;


namespace FPSLogic
{
    public abstract class BaseEnemy : BaseSceneObject, ICollision , ISelectObject
    {
        #region Fields

        [SerializeField] private float _maxHealth;
        private float _currentHealt;
        private bool _isDead;
        private float _timeToDestroy = 10.0f;

        #endregion


        #region Properties

        public float CurrentHealth { get { return _currentHealt; } private set { } }

        public bool IsDead { get { return _isDead; } private set { } }

        public float MaxHealth { get { return _maxHealth; } private set { } }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _currentHealt = _maxHealth;
        }

        #endregion


        #region Methods

        protected virtual float GetDamageToHurt(float initialDamage)
        {
            float damageToHurt = initialDamage;
            if (damageToHurt <= 0) return 0.0f;
            return damageToHurt;
        }

        private void Hurt(float dealingDamage)
        {
            if (_currentHealt > 0)
            {
                _currentHealt -= dealingDamage;
            }

            if (_currentHealt <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Destroy(gameObject, _timeToDestroy);
                _isDead = true;
            }
        }

        #endregion


        #region ICollision

        public virtual void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;

            Hurt(GetDamageToHurt(info.Damage));
        }

        #endregion


        #region ISelectiObject

        public virtual string GetMessage()
        {
            return Name;
        }

        #endregion
    }
}