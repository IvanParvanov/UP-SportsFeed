using Bytes2you.Validation;

using FluentScheduler;

using Ninject;

using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs;

namespace SportsFeed.BackgroundWorkers.ScheduledJobs
{
    public class UpdateDatabaseRegistry : Registry
    {
        public const int DatabaseUpdateIntervalMinutes = 20;
        public const int DatabaseCleanupIntervalMinutes = 4;

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
                                                (s) => s.ToRunEvery(DatabaseUpdateIntervalMinutes).Seconds());
                          })
                .ToRunNow();

            this.Schedule<CleanDatabaseJob>()
                .ToRunNow()
                .AndEvery(DatabaseCleanupIntervalMinutes)
                //.ToRunEvery(DatabaseCleanupIntervalMinutes)
                .Minutes();
        }
    }
}
