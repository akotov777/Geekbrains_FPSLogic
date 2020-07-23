namespace FPSLogic
{
    public abstract class BaseBotState : IBotState
    {
        #region Fields

        internal BotBase _bot;

        #endregion


        #region ClassLifeCycles

        public BaseBotState(BotBase bot)
        {
            _bot = bot;
        }

        #endregion


        #region IBotState

        public abstract void Behave();

        #endregion
    }
}
