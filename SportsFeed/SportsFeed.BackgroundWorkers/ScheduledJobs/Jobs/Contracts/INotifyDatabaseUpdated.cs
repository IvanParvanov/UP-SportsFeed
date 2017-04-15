using System;

using SportsFeed.BackgroundWorkers.EventArgs;

namespace SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts
{
    public interface INotifyDatabaseUpdated
    {
        event EventHandler<DatabaseUpdatedEventArgs> DatabaseUpdated;
    }
}
