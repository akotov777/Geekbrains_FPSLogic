using UnityEngine;


namespace FPSLogic
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _useKey = KeyCode.E;
        private int _mouseButton = (int)MouseButton.LeftButton;
        private int _weaponIndexes;
        private int _scrollWeaponSelector;

        #endregion


        #region Propeties

        public KeyCode UseKey 
        { 
            get 
            { 
                return _useKey; 
            } 
        }

        #endregion


        #region ClassLifeCycles

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
        }

        #endregion


        #region Methods

        private void SelectWeapon(int i)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().GetWeaponFromInventory(i);

            if (tempWeapon != null)
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
        }

        private void SelectNextWeapon()
        {
            SelectWeapon(ScrollIndexIncrease());
        }

        private void SelectPreviousWeapon()
        {
            SelectWeapon(ScrollIndexDecrease());
        }

        private int ScrollIndexIncrease()
        {
            _scrollWeaponSelector++;
            ScrollIndexFailSecure();
            return _scrollWeaponSelector;
        }

        private int ScrollIndexDecrease()
        {
            _scrollWeaponSelector--;
            ScrollIndexFailSecure();
            return _scrollWeaponSelector;
        }

        private void ScrollIndexFailSecure()
        {
            UpdateWeaponIndexes();

            if (_scrollWeaponSelector >= _weaponIndexes)
                _scrollWeaponSelector = 0;

            if (_scrollWeaponSelector < 0)
                _scrollWeaponSelector = _weaponIndexes - 1;
        }

        private void UpdateWeaponIndexes()
        {
            _weaponIndexes = ServiceLocator.Resolve<Inventory>().Weapons.Length;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
                ServiceLocator.Resolve<FlashLightController>()
                    .Switch(ServiceLocator.Resolve<Inventory>().FlashLight);

            if (Input.mouseScrollDelta.y > 0)
                SelectNextWeapon();

            if (Input.mouseScrollDelta.y < 0)
                SelectPreviousWeapon();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SelectWeapon(0);

            if (Input.GetMouseButton(_mouseButton))
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                    ServiceLocator.Resolve<WeaponController>().Fire();

            if(Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }

            if (Input.GetKeyDown(_reloadClip))
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                    ServiceLocator.Resolve<WeaponController>().ReloadClip();
        }

        #endregion
    }
}
