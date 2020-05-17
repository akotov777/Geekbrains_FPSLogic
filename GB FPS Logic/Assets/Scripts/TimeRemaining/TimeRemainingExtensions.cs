using System.Collections.Generic;


namespace FPSLogic
{
    public static partial class TimeRemainingExtensions
    {
        #region Fields

        private static readonly List<ITimeRemaining> _timeRemainings = new List<ITimeRemaining>();

        #endregion


        #region Properties

        public static List<ITimeRemaining> TimeRemainings => _timeRemainings;

        #endregion


        #region Methods

        public static void AddTimeRemaining(this ITimeRemaining value)
        {
            if (_timeRemainings.Contains(value)) return;

            value.CurrentTime = value.Time;
            _timeRemainings.Add(value);
        }

        public static void RemoveTimeRemaining(this ITimeRemaining value)
        {
            if (!_timeRemainings.Contains(value)) return;

            _timeRemainings.Remove(value);
        }

        public static void AddWithReplace(this ITimeRemaining value)
        {
            value.RemoveTimeRemaining();
            value.AddTimeRemaining();
        }

        #endregion
    }
}
