using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemindersForTopEleven
{
    internal class Reminders
    {
        private StartMatchEvents _startMatchEvents;
        private bool _timerStopped;

        internal Reminders(StartMatchEvents startMatchEvents)
        {
            _startMatchEvents = startMatchEvents;
        }

        internal async void StartTimer()
        {
            if (_timerStopped)
                return;

            await StartTimerAsync();
            _startMatchEvents.EvokeStartMatch();
            StartTimer();
        }

        internal void StopTimer()
        {
            _timerStopped = true;
        }

        private async Task StartTimerAsync()
        {
            await Task.Run(() => { Thread.Sleep(60 * 1000); });
        }
    }
}
