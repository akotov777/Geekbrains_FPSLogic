using System;
using UnityEngine;
using static UnityEngine.Random;


namespace FPSLogic
{

    public sealed class FlashLightModel : BaseSceneObject
    {
        #region Fields

        [SerializeField] private float _speed = 11.0f;
        [SerializeField] private float _batteryMaxCharge = 10.0f;
        [SerializeField] private float _chargeSpeed = 1.0f;
        [SerializeField] private float _maxIntensity = 1.0f;

        private Light _light;
        private Transform _objToFollow;
        private Vector3 _offset;
        private float _share;
        private float _takeAwayTheIntensity;

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

        public bool AtLowBattery
        {
            get 
            { 
                return BatteryChargeCurrent <= _batteryMaxCharge / 2.0f;
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
            _light.intensity = _maxIntensity;
            _share = _batteryMaxCharge * 0.25f;
            _takeAwayTheIntensity = _maxIntensity / (_batteryMaxCharge * 100.0f);
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
                case FlashLightActiveType.None:
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
            
            if (_light.intensity < _maxIntensity)
                _light.intensity += _takeAwayTheIntensity;
            else _light.intensity = _maxIntensity;
        }

        public bool EditBatteryCharge()
        {
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;

                if (BatteryChargeCurrent < _share)
                    _light.enabled = Range(0.0f, 100.0f) >= Range(0.0f, 10.0f);
                else
                    _light.intensity -= _takeAwayTheIntensity;

                return true;
            }
            return false;
        }

        #endregion
    }
}