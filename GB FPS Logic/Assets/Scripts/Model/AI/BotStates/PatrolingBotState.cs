using UnityEngine;


namespace FPSLogic
{

    public sealed class PatrolingBotState : BaseBotState
    {
        #region ClassLifeCycles

        public PatrolingBotState(BotBase bot) : base(bot) { }

        #endregion


        #region IBotState

        public override void Behave()
        {
            _bot.MoveToPoint(_bot.PatrolPoint);
            if (_bot.IsAtPoint(_bot.PatrolPoint))
                _bot.SetBotState(_bot.InspectingBotState);
        }

        #endregion
    }
}
