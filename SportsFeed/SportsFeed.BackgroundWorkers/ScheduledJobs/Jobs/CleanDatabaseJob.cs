using System;

using Bytes2you.Validation;

using SportsFeed.BackgroundWorkers.EventArgs;
using SportsFeed.BackgroundWorkers.Helpers.Contracts;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Base;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts;
using SportsFeed.Services.Contracts;

namespace SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs
{
    public class CleanDatabaseJob : JobBase, INotifyDatabaseCleanup
    {
        public event EventHandler<DatabaseCleanedEventArgs> DatabaseCleaned;

        private readonly IDbCleanupService dbCleanupService;

        public CleanDatabaseJob(IHostingEnvironmentRegister hostingEnvironment, IDbCleanupService dbCleanupService)
            :base(hostingEnvironment)
        {
            Guard.WhenArgument(dbCleanupService, nameof(dbCleanupService)).IsNull().Throw();

            this.dbCleanupService = dbCleanupService;
        }

        public override void Execute()
        {
            lock (this.Locker)
            {
                if (this.ShuttingDown)
                {
                    return;
                }

                var changes = this.dbCleanupService.CleanStaleData();

                this.DatabaseCleaned?.Invoke(this, new DatabaseCleanedEventArgs(changes));
            }
        }

        public bool HasSubscribers()
        {
            return this.DatabaseCleaned != null;
        }
    }
}
