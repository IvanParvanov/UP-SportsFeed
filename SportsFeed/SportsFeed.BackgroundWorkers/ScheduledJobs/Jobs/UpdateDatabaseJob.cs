using System;

using Bytes2you.Validation;

using SportsFeed.BackgroundWorkers.EventArgs;
using SportsFeed.BackgroundWorkers.Helpers.Contracts;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Base;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts;
using SportsFeed.Services.Contracts;

namespace SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs
{
    public class UpdateDatabaseJob : JobBase, INotifyDatabaseUpdated
    {
        public event EventHandler<DatabaseUpdatedEventArgs> DatabaseUpdated;

        private readonly IDbSyncService dbSyncService;

        public UpdateDatabaseJob(IHostingEnvironmentRegister hostingEnvironment, IDbSyncService dbSyncService)
            :base(hostingEnvironment)
        {
            Guard.WhenArgument(dbSyncService, nameof(dbSyncService)).IsNull().Throw();

            this.dbSyncService = dbSyncService;
        }

        public override void Execute()
        {
            lock (this.Locker)
            {
                if (this.ShuttingDown)
                {
                    return;
                }

                var changes = this.dbSyncService.SyncDatabase();

                this.DatabaseUpdated?.Invoke(this, new DatabaseUpdatedEventArgs(changes));
            }
        }

        public bool HasSubscribers()
        {
            return this.DatabaseUpdated != null;
        }
    }
}
