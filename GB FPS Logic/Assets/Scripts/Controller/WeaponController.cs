namespace FPSLogic
{
    public sealed class WeaponController : BaseController
    {
        #region Fields

        private Weapon _weapon;

        #endregion


        #region Methods

        public override void On(params BaseSceneObject[] weapon)
        {
            if (IsActive) return;
            if (weapon.Length > 0) _weapon = weapon[0] as Weapon;
            if (_weapon == null) return;
            base.On(_weapon);
            _weapon.IsVisible = true;
            _weapon.SetActive(true);
            UI.WeaponUIText.SetActive(true);
            UI.WeaponUIText.ShowData(_weapon.Clip.CountAmmunition, _weapon.ClipCount);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _weapon.IsVisible = false;
            _weapon.SetActive(false);
            _weapon = null;
            UI.WeaponUIText.SetActive(false);
        }

        public void Fire()
        {
            _weapon.Fire();
            UI.WeaponUIText.ShowData(_weapon.Clip.CountAmmunition, _weapon.ClipCount);
        }

        public void ReloadClip()
        {
            _weapon.ReloadClip();
            UI.WeaponUIText.ShowData(_weapon.Clip.CountAmmunition, _weapon.ClipCount);
        }

        #endregion
    }
}