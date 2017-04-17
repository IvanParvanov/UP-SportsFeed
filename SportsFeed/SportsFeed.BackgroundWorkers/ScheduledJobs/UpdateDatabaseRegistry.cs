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
            //this.Schedule(() =>
            //              {
            //                  var job = kernel.Get<UpdateDatabaseJob>(typeof(UpdateDatabaseJob).Name);
            //                  job.DatabaseUpdated += this.JobOnDatabaseUpdated;

            //                  job.Execute();

            //                  job.DatabaseUpdated -= this.JobOnDatabaseUpdated;
            //              })
            //    .ToRunNow();

            this.Schedule<UpdateDatabaseJob>()
                // Workaround for https://github.com/fluentscheduler/FluentScheduler/issues/107
                .ToRunEvery(DatabaseUpdateIntervalMinutes)
                .Minutes();
            //.ToRunNow();
            //.AndEvery(DatabaseUpdateIntervalMinutes)
            //.Minutes();

            //this.Schedule<CleanDatabaseJob>()
            //    .ToRunNow()
            //    .AndEvery(DatabaseCleanupIntervalMinutes)
            //    .Minutes();
        }

        private void JobOnDatabaseUpdated(object sender, DatabaseUpdatedEventArgs databaseUpdatedEventArgs)
        {
            this.Schedule<UpdateDatabaseJob>()
                .ToRunEvery(DatabaseUpdateIntervalMinutes)
                .Minutes();
        }
    }
}
