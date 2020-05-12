using UnityEngine;
using UnityEngine.UI;


namespace FPSLogic
{
    public sealed class FlashLightUI : MonoBehaviour
    {
        #region Fields

        private Text _text;
        private Image _chargeBar;

        #endregion


        #region Properties

        public float Text
        {
            set => _text.text = $"{value:0.0}";
        }

        public float ChargeBarFillAmount
        {
            set => _chargeBar.fillAmount = value;
        }

        #endregion
        
        
        #region UnityMethods

        private void Awake()
        {
            _text = GetComponentInChildren<Text>();
            _chargeBar = GetComponentInChildren<Image>();
        }

        #endregion


        #region Methods

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
            _chargeBar.gameObject.SetActive(value);
        }

        #endregion
    }
}
