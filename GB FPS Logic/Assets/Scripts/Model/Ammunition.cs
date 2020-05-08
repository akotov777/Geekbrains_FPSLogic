using UnityEngine;


namespace FPSLogic
{
    public abstract class Ammunition : BaseSceneObject
    {
        #region Fields

        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;
        [SerializeField] private float _lossDamageAtTime = 0.2f;
        protected float _currentDamage;
        private ITimeRemaining _timeRemaining;

        public AmmunitionType Type = AmmunitionType.Bullet;
        static public readonly Vector3 StackPosition = new Vector3(-9000.0f, -9000.0f, -9000.0f);

        #endregion


        #region Properties

        public float TimeToDestruct { get { return _timeToDestruct; } }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _currentDamage = _baseDamage;
        }

        private void OnEnable()
        {
            _timeRemaining = new TimeRemaining(LossOfDamageAtTime, 1.0f, true);
            _timeRemaining.AddTimeRemaining();
        }

        private void OnDisable()
        {
            _timeRemaining?.RemoveTimeRemaining();
        }

        #endregion


        #region Methods

        public void AddForce(Vector3 direction)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(direction);
        }

        private void LossOfDamageAtTime()
        {
            _currentDamage -= _lossDamageAtTime;
        }

        private protected void LossOfDamageByPenetrating(float value)
        {
            _currentDamage -= value;
        }

        private protected void DestroyIfNegativeDamage()
        {
            if (_currentDamage <= 0.0f) DestroyAmmunition();
        }

        private protected void ReturnToPool()
        {
            enabled = false;
            gameObject.SetActive(false);
            Rigidbody.velocity = Vector3.zero;
            Transform.position = StackPosition;
        }

        public void InitializeAmmunition(Transform parent, Quaternion rotation)
        {
            _currentDamage = _baseDamage;
            Transform.position = parent.position;
            Transform.rotation = rotation;
            enabled = true;
            gameObject.SetActive(true);
            Invoke("DestroyAmmunition", _timeToDestruct);
        }

        private protected void DestroyAmmunition()
        {
            ReturnToPool();
        }

        #endregion
    }
}