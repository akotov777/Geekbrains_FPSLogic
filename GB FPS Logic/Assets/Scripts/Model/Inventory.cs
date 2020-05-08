using UnityEngine;


namespace FPSLogic
{
    public sealed class Inventory : IInitialization
    {
        #region Fields

        private Weapon[] _weapons = new Weapon[5];

        #endregion


        #region Properties

        public Weapon[] Weapons => _weapons;

        public FlashLightModel FlashLight { get; private set; }

        #endregion


        #region Methods

        public Weapon GetWeaponFromInventory(int i)
        {
            return Weapons[i];
        }

        public void RemoveWeapon(int i)
        {
            _weapons[i] = null;
        }

        public void AddWeapon(Weapon weapon)
        {
            for (var i = 0; i < Weapons.Length; i++)
                if (Weapons[i] == null)
                {
                    var tempTransform = GameObject.FindGameObjectWithTag(TagManager.WeaponBindingPoint)
                                                  .transform;
                    Weapons[i] = weapon;
                    Weapons[i].SetPickUpState(false);
                    Weapons[i].SetActive(false);
                    Weapons[i].transform.SetParent(tempTransform);
                    Weapons[i].transform.position = tempTransform.position;
                    Weapons[i].transform.rotation = tempTransform.rotation;
                    Weapons[i].InitializeAmmunition(Weapons[i].MaxCountAmmunition);
                    return;
                }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            var tempWeapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
                GetComponentsInChildren<Weapon>();

            foreach (var weapon in tempWeapons)
                AddWeapon(weapon);

            FlashLight = Object.FindObjectOfType<FlashLightModel>();
            FlashLight.Switch(FlashLightActiveType.Off);
        }

        #endregion
    }
}