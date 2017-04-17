using System.Collections.Generic;

using SportsFeed.Services.Results;

namespace SportsFeed.Services.Factories
{
    public interface IDbUpdatedResultFactory
    {
        IDatabaseUpdatedResult CreateDatabaseUpdatedResult();

        IDatabaseUpdatedResult CreateDatabaseUpdatedResult(ICollection<int> sportIds,
                                                           ICollection<int> eventIds,
                                                           ICollection<int> matchIds,
                                                           ICollection<int> betIds,
                                                           ICollection<int> oddIds);

        IDatabaseCleanedResult CreateDatabaseCleanedResult(ICollection<int> eventIds,
                                                           ICollection<int> matchIds,
                                                           ICollection<int> betIds);

        IDatabaseCleanedResult CreateDatabaseCleanedResult();
    }
}
