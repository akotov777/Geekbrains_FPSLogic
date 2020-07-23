using UnityEngine;


namespace FPSLogic
{

    public sealed class InspectingBotState : BaseBotState
    {
        #region ClassLifeCycles

        public InspectingBotState(BotBase bot) : base(bot) { }

        #endregion


        #region IBotState

        public override void Behave()
        {
            _bot.Color = Color.blue;
            _bot.ChangeStateAfterInspecting.AddTimeRemaining();
        }

        #endregion
    }
}
