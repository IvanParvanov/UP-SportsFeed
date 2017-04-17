using System.Collections.Generic;

namespace SportsFeed.Services.Results
{
    public interface IDatabaseCleanedResult
    {
        ISet<int> EventIds { get; }

        ISet<int> MatchIds { get; }

        ISet<int> BetIds { get; }
    }
}