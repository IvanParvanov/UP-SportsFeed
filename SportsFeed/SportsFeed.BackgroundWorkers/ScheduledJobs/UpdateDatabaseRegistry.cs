using FluentScheduler;

using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs;

namespace SportsFeed.BackgroundWorkers.ScheduledJobs
{
    public class UpdateDatabaseRegistry : Registry
    {
        public const int DatabaseUpdateIntervalMinutes = 1;
        public const int DatabaseCleanupIntervalMinutes = 10;

        public UpdateDatabaseRegistry()
        {
            this.UseUtcTime();
            this.NonReentrantAsDefault();

            // Workaround for https://github.com/fluentscheduler/FluentScheduler/issues/107
            this.Schedule<UpdateDatabaseJob>()
                //.ToRunEvery(DatabaseUpdateIntervalMinutes)
                .ToRunNow();
            //.AndEvery(DatabaseUpdateIntervalMinutes)
            //.Minutes();

            //this.Schedule<CleanDatabaseJob>()
            //    .ToRunNow()
            //    .AndEvery(DatabaseCleanupIntervalMinutes)
            //    .Minutes();
        }
    }
}
