using System.Collections.Generic;

using SportsFeed.Services.Results;

namespace SportsFeed.Services.Factories
{
    public interface IDbUpdatedResultFactory
    {
        IDatabaseUpdatedResult CreateDatabaseUpdatedResult();

        IDatabaseUpdatedResult CreateDatabaseUpdatedResult(
            ISet<int> sportIds,
            ISet<int> eventIds,
            ISet<int> matchIds,
            ISet<int> betIds,
            ISet<int> oddIds);
    }
}
