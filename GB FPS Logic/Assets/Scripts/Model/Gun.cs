namespace FPSLogic
{
    public sealed class Gun : Weapon
    {
        #region Methods

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;
            var tempAmmunition = GetNextAvailableInstance();
            if (tempAmmunition != null) tempAmmunition.InitializeAmmunition(_barrel, _barrel.rotation);
            tempAmmunition.AddForce(_barrel.forward * _force);
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }

        #endregion
    }
}