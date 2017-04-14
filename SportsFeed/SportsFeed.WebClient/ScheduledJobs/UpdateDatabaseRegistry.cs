using FluentScheduler;

using SportsFeed.WebClient.ScheduledJobs.Jobs;

namespace SportsFeed.WebClient.ScheduledJobs
{
    public class UpdateDatabaseRegistry : Registry
    {
        public const int DatabaseUpdateIntervalSeconds = 60;

        public UpdateDatabaseRegistry()
        {
            this.UseUtcTime();
            this.NonReentrantAsDefault();

            this.Schedule<UpdateDatabaseJob>()
                .ToRunNow()
                .AndEvery(DatabaseUpdateIntervalSeconds)
                .Seconds();
        }
    }
}
