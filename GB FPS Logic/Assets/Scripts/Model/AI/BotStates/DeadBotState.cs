using UnityEngine;


namespace FPSLogic
{

    public sealed class DeadBotState : BaseBotState
    {
        #region ClassLifeCycles

        public DeadBotState(BotBase bot) : base(bot) { }

        #endregion


        #region IBotState

        public override void Behave()
        {

        }

        #endregion
    }
}
