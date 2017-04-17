using SportsFeed.Services.Results;

namespace SportsFeed.Services.Contracts
{
    public interface IDbSyncService
    {
        IDatabaseUpdatedResult SyncDatabase();
    }
}
