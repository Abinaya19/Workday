using System;
using System.Diagnostics;
using System.Timers;

namespace Workday
{
    public class TimerExpireEventArgs: EventArgs 
    {
        public TimeSpan BlockTotal { get; set; }

        public TimeSpan WorkdayTotal { get; set; }
    }

    public class WorkdayModel
    {
        private Stopwatch _blockStopWatch;
        private Timer _blockEventTimer;
        private TimeSpan? _workdayTotal;

        public event EventHandler<TimerExpireEventArgs> TimerExpired;

        public bool WorkdayStarted
        {
            get
            {
                return _workdayTotal.HasValue;
            }
        }

        public bool BlockStarted
        {
            get
            {
                return _blockStopWatch.IsRunning;
            }
        }

        public WorkdayModel()
        {
            _blockStopWatch = new Stopwatch();
            _blockEventTimer = new Timer(1000);
            _blockEventTimer.Elapsed += (sender, args) =>
            {
                OnTimerExpired();
            };
            _blockEventTimer.Start();
        }

        public void StartWorkday()
        {
            _workdayTotal = new TimeSpan();
        }

        public void StopWorkday()
        {
            if (BlockStarted)
            {
                StopBlock();
            }

            _workdayTotal = null;
            OnTimerExpired(); 
        }

        public void StartBlock()
        {
            if (!WorkdayStarted) {
                return;
            }

            _blockStopWatch.Start();
        }

        public void StopBlock()
        {
            _blockStopWatch.Stop();
            var timeSpan = _blockStopWatch.Elapsed;
            _blockStopWatch.Reset();

            _workdayTotal = _workdayTotal.Value.Add(timeSpan);

            OnTimerExpired();
        }

        private void OnTimerExpired()
        {
            TimerExpired?.Invoke(this, new TimerExpireEventArgs()
            {
                BlockTotal = _blockStopWatch.Elapsed,
                WorkdayTotal = _workdayTotal.HasValue ? _workdayTotal.Value.Add(_blockStopWatch.Elapsed) : default(TimeSpan)
            });
        }
    }
}
