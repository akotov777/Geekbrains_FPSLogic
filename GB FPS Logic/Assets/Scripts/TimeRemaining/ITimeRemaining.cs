using System;


namespace FPSLogic
{
    public interface ITimeRemaining
    {
        #region Properties

        Action Method { get; }
        bool IsRepeating { get; }
        float Time { get; }
        float CurrentTime { get; set; }

        #endregion
    }
}
