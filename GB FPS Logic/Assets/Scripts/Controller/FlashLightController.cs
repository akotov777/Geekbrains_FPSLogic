using UnityEngine;


namespace FPSLogic
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private FlashLightModel _flashLightModel;
        private FlashLightUI _flashLightUI;

        #endregion


        #region Methods

        public override void On()
        {
            if (IsActive) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On();
            _flashLightModel.Switch(FlashLightActiveType.On);
            _flashLightUI.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);
            _flashLightUI.SetActive(false);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive)
            {
                _flashLightModel.ChargeBattery();
                _flashLightUI.ChargeBarFillAmount = _flashLightModel.BatteryChargeProportion;
                return;
            }

            _flashLightModel.Rotation();

            if (_flashLightModel.EditBatteryCharge())
            {
                _flashLightUI.Text = _flashLightModel.BatteryChargeCurrent;
                _flashLightUI.ChargeBarFillAmount = _flashLightModel.BatteryChargeProportion;
            }
            else
            {
                Off();
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
            _flashLightUI = Object.FindObjectOfType<FlashLightUI>();
        }

        #endregion
    }
}
