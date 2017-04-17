using System;

using Bytes2you.Validation;

using FluentScheduler;

using Ninject;

using SportsFeed.BackgroundWorkers.EventArgs;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs;

namespace SportsFeed.BackgroundWorkers.ScheduledJobs
{
    public class UpdateDatabaseRegistry : Registry
    {
        public const int DatabaseUpdateIntervalMinutes = 1;
        public const int DatabaseCleanupIntervalMinutes = 10;

        public UpdateDatabaseRegistry(IKernel kernel)
        {
            Guard.WhenArgument(kernel, nameof(kernel)).IsNull().Throw();

            this.UseUtcTime();
            this.NonReentrantAsDefault();

            // Workaround for https://github.com/fluentscheduler/FluentScheduler/issues/107
            this.Schedule(() =>
                          {
                              var job = kernel.Get<UpdateDatabaseJob>();
                              job.Execute();

                              JobManager.AddJob(() => kernel.Get<UpdateDatabaseJob>().Execute(),
                                                (s) => s.ToRunEvery(DatabaseUpdateIntervalMinutes).Minutes());
                          })
                .ToRunNow();

            //this.Schedule<CleanDatabaseJob>()
            //    .ToRunNow()
            //    .AndEvery(DatabaseCleanupIntervalMinutes)
            //    .Minutes();
        }
    }
}
