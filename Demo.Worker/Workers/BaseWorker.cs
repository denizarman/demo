using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Worker.Workers
{
    public abstract class BaseWorker : IHostedService
    {
        // https://crontab.guru/
        protected abstract string CronExpression { get; }

        private readonly ILogger<BaseWorker> _logger;
        private readonly CrontabSchedule _crontabSchedule;
        private DateTime _nextRun;

        public BaseWorker(ILogger<BaseWorker> logger)
        {
            _logger = logger;
            _crontabSchedule = CrontabSchedule.Parse(CronExpression, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
        }

        public abstract Task Execute();

        public Task StartAsync(CancellationToken cancellationToken)
        {

            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(UntilNextExecution(), cancellationToken); // wait until next time

                    _logger.LogInformation("Worker executing at: {time}", DateTimeOffset.Now);

                    await Execute();

                    _logger.LogInformation("Worker executed at: {time}", DateTimeOffset.Now);

                    _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
                    _logger.LogInformation("Worker next execution is scheduled at : {time}", _nextRun);
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private int UntilNextExecution() => Math.Max(0, (int)_nextRun.Subtract(DateTime.Now).TotalMilliseconds);
    }
}
