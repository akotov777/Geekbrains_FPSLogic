using System;
using UnityEngine;


namespace FPSLogic
{

    public sealed class FlashLightModel : BaseSceneObject
    {
        #region Fields

        [SerializeField] private float _speed = 11.0f;
        [SerializeField] private float _batteryMaxCharge = 10.0f;
        [SerializeField] private float _chargeSpeed = 1.0f;

        private Light _light;
        private Transform _objToFollow;
        private Vector3 _offset;

        #endregion


        #region Properties

        public float BatteryChargeCurrent { get; private set; }

        public float BatteryChargeProportion
        {
            get
            {
                return BatteryChargeCurrent / _batteryMaxCharge;
            }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
            _objToFollow = Camera.main.transform;
            _offset = Transform.position - _objToFollow.position;
            BatteryChargeCurrent = _batteryMaxCharge;
        }

        #endregion


        #region Methods

        public void Switch(FlashLightActiveType value)
        {
            switch (value)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    Transform.position = _objToFollow.position + _offset;
                    Transform.rotation = _objToFollow.rotation;
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void Rotation()
        {
            Transform.position = _objToFollow.position + _offset;
            Transform.rotation = Quaternion.Lerp(Transform.rotation,
                _objToFollow.rotation, _speed * Time.deltaTime);
        }

        public void ChargeBattery()
        {
            if (BatteryChargeCurrent >= _batteryMaxCharge) return;

            BatteryChargeCurrent += Time.deltaTime * _chargeSpeed;
        }

        public bool EditBatteryCharge()
        {
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;
                return true;
            }
            return false;
        }

        #endregion
    }
}