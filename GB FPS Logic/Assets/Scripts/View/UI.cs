using UnityEngine;


namespace FPSLogic
{
    public sealed class UI
    {
        #region Fields

        private FlashLightUI _flashLightUI;
        private WeaponUIText _weaponUIText;
        private SelectionObjMessageUI _selectionObjMessageUI;

        #endregion


        #region Properties

        public FlashLightUI FlashLightUI
        {
            get
            {
                if (!_flashLightUI)
                    _flashLightUI = Object.FindObjectOfType<FlashLightUI>();
                return _flashLightUI;
            }
        }

        public WeaponUIText WeaponUIText
        {
            get
            {
                if (!_weaponUIText)
                    _weaponUIText = Object.FindObjectOfType<WeaponUIText>();
                return _weaponUIText;
            }
        }

        public SelectionObjMessageUI SelectionObjMessageUI
        {
            get
            {
                if (!_selectionObjMessageUI)
                    _selectionObjMessageUI = Object.FindObjectOfType<SelectionObjMessageUI>();
                return _selectionObjMessageUI;
            }
        }

        #endregion
    }
}