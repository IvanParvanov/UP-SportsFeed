using System.Collections.Generic;

namespace SportsFeed.Services.Results
{
    public interface IDatabaseUpdatedResult : IDatabaseCleanedResult
    {
        ISet<int> SportIds { get; }

        ISet<int> OddIds { get; }
    }
}