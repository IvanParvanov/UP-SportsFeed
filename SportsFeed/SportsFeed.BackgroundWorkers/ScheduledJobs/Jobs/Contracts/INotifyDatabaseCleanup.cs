using System;

using SportsFeed.BackgroundWorkers.EventArgs;

namespace SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts
{
    public interface INotifyDatabaseCleanup
    {
        event EventHandler<DatabaseCleanedEventArgs> DatabaseCleaned;

        bool HasSubscribers();
    }
}
