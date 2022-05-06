namespace Api.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Cronos;
    using Microsoft.Extensions.Hosting;
    using Timer = System.Timers.Timer;

    public abstract class CronJobService : IHostedService, IDisposable
    {
        private System.Timers.Timer? _timer;
        private readonly CronExpression _expression;
        private readonly bool _instantRun;

        protected CronJobService(string cronExpression, bool instantRun)
        {
            _expression = CronExpression.Parse(cronExpression);
            _instantRun = instantRun;
        }

#pragma warning disable CS1998 // we're not awaiting this on purpose to avoid blocking IIS startup
        public virtual async Task StartAsync(CancellationToken cancellationToken)
#pragma warning restore CS1998
        {
#pragma warning disable 4014 // we're not awaiting this on purpose to avoid blocking IIS startup
            ScheduleJob(cancellationToken);
#pragma warning restore 4014
        }

        private async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                // prevent non-positive values from being passed into Timer
                if (delay.TotalMilliseconds <= 0)
                {
                    await ScheduleJob(cancellationToken);
                }

                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    // reset and dispose timer
                    _timer.Dispose();
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await DoWork(cancellationToken);
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJob(cancellationToken);
                    }
                };

                if (_instantRun)
                {
                    await DoWork(cancellationToken);
                }

                _timer.Start();
            }

            await Task.CompletedTask;
        }

        protected abstract Task DoWork(CancellationToken cancellationToken);

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
