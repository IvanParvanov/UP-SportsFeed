using System.Collections.Generic;

using SportsFeed.Models.Contracts;
using SportsFeed.Services.Results;

namespace SportsFeed.Services.Contracts
{
    public interface IDbSyncService
    {
        IDatabaseUpdatedResult SyncDatabase();
    }
}
