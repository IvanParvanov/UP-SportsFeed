using System.Collections.Generic;

using SportsFeed.Models.Contracts;

namespace SportsFeed.Services.Contracts
{
    public interface IDbSyncService
    {
        IEnumerable<IExternalEntity> SyncDatabase();
    }
}
