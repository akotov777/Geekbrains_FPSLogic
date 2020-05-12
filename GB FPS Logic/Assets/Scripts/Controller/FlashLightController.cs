using UnityEngine;


namespace FPSLogic
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private FlashLightModel _flashLightModel;

        #endregion


        #region Methods

        public override void On(params BaseSceneObject[] flashLight)
        {
            if (IsActive) return;
            if (flashLight.Length > 0)
                _flashLightModel = flashLight[0] as FlashLightModel;

            if (_flashLightModel == null) return;

            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On(_flashLightModel);
            _flashLightModel.Switch(FlashLightActiveType.On);
            UI.FlashLightUI.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);
            UI.FlashLightUI.SetActive(false);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (_flashLightModel == null) return;
            if (!IsActive)
            {
                _flashLightModel.ChargeBattery();
                UI.FlashLightUI.ChargeBarFillAmount = _flashLightModel.BatteryChargeProportion;
                return;
            }

            _flashLightModel.Rotation();

            if (_flashLightModel.EditBatteryCharge())
            {
                UI.FlashLightUI.Text = _flashLightModel.BatteryChargeCurrent;
                UI.FlashLightUI.ChargeBarFillAmount = _flashLightModel.BatteryChargeProportion;
            }
            else
                Off();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            UI.FlashLightUI.SetActive(false);
        }

        #endregion
    }
}
