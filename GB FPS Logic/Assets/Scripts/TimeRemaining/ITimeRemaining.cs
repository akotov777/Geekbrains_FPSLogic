using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
