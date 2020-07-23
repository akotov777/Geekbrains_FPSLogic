using UnityEngine;


namespace FPSLogic
{

    public sealed class HasADetectedTargetBotState : BaseBotState
    {
        #region ClassLifeCycles

        public HasADetectedTargetBotState(BotBase bot) : base(bot) { }

        #endregion


        #region IBotState

        public override void Behave()
        {
            _bot.MoveToPoint(_bot.Target.position);
            _bot.FireWeapon();
            if (!_bot.IsSeeingEnemy)
            {
                _bot.SetLastTargetPosition();
                _bot.CurrentBotState = _bot.HasLostEnemyBotState;
            }
        }

        #endregion
    }
}
