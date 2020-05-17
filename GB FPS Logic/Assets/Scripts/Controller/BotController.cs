using System.Collections.Generic;
using UnityEngine;


namespace FPSLogic
{
    public sealed class BotController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private readonly List<BotBase> _botList = new List<BotBase>();

        #endregion


        #region Methods

        private void AddBotToList(BotBase bot)
        {
            if (!_botList.Contains(bot))
            {
                _botList.Add(bot);
                bot.OnDieChange += RemoveBotFromList;
            }
        }

        private void RemoveBotFromList(BotBase bot)
        {
            if (!_botList.Contains(bot)) return;

            bot.OnDieChange -= RemoveBotFromList;
            _botList.Remove(bot);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;

            foreach(BotBase bot in _botList)
                bot.Execute();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            var bots = GameObject.FindObjectsOfType<BotBase>();

            foreach(BotBase bot in bots)
                AddBotToList(bot);
        }

        #endregion
    }
}