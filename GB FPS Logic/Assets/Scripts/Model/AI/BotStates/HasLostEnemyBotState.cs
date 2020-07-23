using UnityEngine;


namespace FPSLogic
{

    public sealed class HasLostTargetBotState : BaseBotState
    {
        #region ClassLifeCycles

        public HasLostTargetBotState(BotBase bot) : base(bot) { }

        #endregion


        #region IBotState

        public override void Behave()
        {
            _bot.MoveToPoint(_bot.LastTargetPosition);
            if (_bot.IsAtPoint(_bot.LastTargetPosition))
                _bot.CurrentBotState = _bot.InspectingBotState;
        }

        #endregion
    }
}
