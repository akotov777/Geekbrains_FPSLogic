using UnityEngine;
using UnityEngine.UI;


namespace FPSLogic
{
    public sealed class WeaponUIText : MonoBehaviour
    {
        #region Fields

        private Text _text;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        #endregion


        #region Methods

        public void ShowData(int ammunitionCount, int clipCount)
        {
            _text.text = $"{ammunitionCount}/{clipCount}";
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }

        #endregion
    }
}