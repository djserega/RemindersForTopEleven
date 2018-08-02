using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindersForTopEleven
{
    internal delegate void StartMatch();

    internal class StartMatchEvents : EventArgs
    {
        internal event StartMatch StartMatch;
        internal void EvokeStartMatch() => StartMatch?.Invoke();
    }
}
