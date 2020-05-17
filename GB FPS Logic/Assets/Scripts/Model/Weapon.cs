using System.Collections.Generic;
using UnityEngine;


namespace FPSLogic
{
    public abstract class Weapon : BaseSceneObject, ISelectObject
    {
        #region Fields

        [SerializeField] private Ammunition Ammunition;
        private int _maxAmmunitionCount = 40;
        private int _minAmmunitionCount = 20;

        private int _clipCount = 5;
        public Clip Clip;

        public AmmunitionType[] AmmunitionTypes;

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected float _force = 999;
        [SerializeField] protected float _fireRate = 5.0f;
        private Queue<Clip> _clips = new Queue<Clip>();

        protected bool _isReady = true;
        protected ITimeRemaining _timeRemaining;

        private List<Ammunition> _pooledAmmunition = new List<Ammunition>();

        private bool _canPickUp;

        #endregion


        #region Properties

        public int ClipCount => _clips.Count;

        public int MaxCountAmmunition { get { return _maxAmmunitionCount; } }

        public float FireRate { get { return 60.0f / _fireRate; } }

        public bool CanPickUp { get { return _canPickUp; } }

        #endregion


        #region UnityMerhods

        private void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyToShoot, FireRate);
            for (var i = 0; i <= _clipCount; i++)
                AddClip(new Clip { CountAmmunition = Random.Range(_minAmmunitionCount, _maxAmmunitionCount) });

            _canPickUp = true;

            ReloadClip();
        }

        #endregion


        #region Methods

        public abstract void Fire();

        protected void ReadyToShoot()
        {
            _isReady = true;
        }

        protected void AddClip(Clip clip)
        {
            _clips.Enqueue(clip);
        }

        public void ReloadClip()
        {
            if (ClipCount <= 0) return;
            Clip = _clips.Dequeue();
        }

        public void InitializeAmmunition(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _pooledAmmunition.Add(Instantiate
                                      (Ammunition, Ammunition.StackPosition, Quaternion.identity));
                _pooledAmmunition[i].enabled = false;
                _pooledAmmunition[i].gameObject.SetActive(false);
            }
        }

        private protected Ammunition GetNextAvailableInstance()
        {
            foreach(Ammunition am in _pooledAmmunition)
                if (!am.enabled)
                    return am;

            return GetNextAvailableInstance(true);
        }

        private protected Ammunition GetNextAvailableInstance(bool instantiateNew)
        {
            if(instantiateNew)
                InstantiateNewAmmunition();
            return GetNextAvailableInstance();
        }

        private void InstantiateNewAmmunition()
        {
            if (_pooledAmmunition.Count < 1)
                InitializeAmmunition(_maxAmmunitionCount);
            else
                InitializeAmmunition(_pooledAmmunition.Count);
        }

        public void SetPickUpState(bool value)
        {
            _canPickUp = value;
        }

        #endregion


        #region ISelectObject

        public virtual string GetMessage()
        {
            return Name;
        }

        #endregion
    }
}