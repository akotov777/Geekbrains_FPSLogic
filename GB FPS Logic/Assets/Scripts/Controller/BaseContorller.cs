namespace FPSLogic
{
    public abstract class BaseController
    {
        #region Fields

        protected UI UI;

        #endregion


        #region Properties

        public bool IsActive { get; private set; }

        #endregion


        #region ClassLifeCycles

        protected BaseController()
        {
            UI = new UI();
        }

        #endregion


        #region Methods

        public virtual void On()
        {
            On(null);
        }

        public virtual void On(params BaseSceneObject[] objects)
        {
            IsActive = true;
        }

        public virtual void Off()
        {
            IsActive = false;
        }

        public void Switch(params BaseSceneObject[] objects)
        {
            if (!IsActive) On(objects);
            else Off();
        }

        #endregion
    }
}