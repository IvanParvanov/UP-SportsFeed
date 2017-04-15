using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

using Bytes2you.Validation;

using FluentScheduler;

using SportsFeed.BackgroundWorkers.EventArgs;
using SportsFeed.BackgroundWorkers.Helpers.Contracts;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts;
using SportsFeed.Services.Contracts;

namespace SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs
{
    public class UpdateDatabaseJob : IJob, IRegisteredObject, INotifyDatabaseUpdated
    {
        public event EventHandler<DatabaseUpdatedEventArgs> DatabaseUpdated;

        private readonly IDbSyncService dbSyncService;

        private readonly IHostingEnvironmentRegister hostingEnvironment;

        private readonly object locker = new object();

        private bool shuttingDown;

        public UpdateDatabaseJob(IDbSyncService dbSyncService, IHostingEnvironmentRegister hostingEnvironment)
        {
            Guard.WhenArgument(dbSyncService, nameof(dbSyncService)).IsNull().Throw();
            Guard.WhenArgument(hostingEnvironment, nameof(hostingEnvironment)).IsNull().Throw();

            this.dbSyncService = dbSyncService;
            this.hostingEnvironment = hostingEnvironment;
            this.hostingEnvironment.Register(this);
        }

        public void Execute()
        {
            lock (this.locker)
            {
                if (this.shuttingDown)
                {
                    return;
                }

                var changes = this.dbSyncService.SyncDatabase();

                this.DatabaseUpdated?.Invoke(this, new DatabaseUpdatedEventArgs(changes));
            }
        }

        public void Stop(bool immediate)
        {
            lock (this.locker)
            {
                this.shuttingDown = true;
            }

            this.hostingEnvironment.Unregister(this);
        }
    }
}
