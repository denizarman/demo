using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Demo.Worker.Workers
{
    public class DummyWorker : BaseWorker
    {
        protected override string CronExpression => "0/5 * * * * *"; // 5'in katı saniyelerde 05, 10, 15, 20 ....
        private readonly ILogger<DummyWorker> _logger;

        private int taskRunningDurationInSeconds = 10;
        private int iterationCount = 0;

        public DummyWorker(ILogger<DummyWorker> logger) : base(logger)
        {
            _logger = logger;
        }

        public override async Task Execute()
        {
            iterationCount++;

            _logger.LogInformation("I am running task, i will be working for next {second} seconds, this is my {iteration}. iteration, please wish me luck !!! at : {datetime}", taskRunningDurationInSeconds, iterationCount, DateTimeOffset.Now);
            await Task.Delay(taskRunningDurationInSeconds * 1000);
            _logger.LogInformation("My {iteration} iteration is completed, see you in next one !!! at : {datetime}", iterationCount, DateTimeOffset.Now);
        }
    }
}
