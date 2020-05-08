using UnityEngine;


namespace FPSLogic
{
    public sealed class Controllers : IInitialization
    {
        #region Fields

        private readonly IExecute[] _controllersToExecute;

        #endregion


        #region Properties

        public int Length => _controllersToExecute.Length;

        public IExecute this[int index] => _controllersToExecute[index];

        #endregion


        #region ClassLifeCycles

        public Controllers()
        {
            IMotor motor = default;

            switch (Application.platform)
            {
                default:
                    motor = new Motor(
                        ServiceLocatorMonoBehaviour
                        .GetService<CharacterController>());
                    break;
            }

            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new SelectionController());

            _controllersToExecute = new IExecute[5];

            _controllersToExecute[0] = ServiceLocator.Resolve<PlayerController>();
            _controllersToExecute[1] = ServiceLocator.Resolve<FlashLightController>();
            _controllersToExecute[2] = ServiceLocator.Resolve<InputController>();
            _controllersToExecute[3] = ServiceLocator.Resolve<TimeRemainingController>();
            _controllersToExecute[4] = ServiceLocator.Resolve<SelectionController>();
        }

        #endregion


        #region IInitialization

        public void Initialization() 
        {
            foreach (var controller in _controllersToExecute)
            {
                if(controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }

            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<SelectionController>().On();
        }

        #endregion
    }
}