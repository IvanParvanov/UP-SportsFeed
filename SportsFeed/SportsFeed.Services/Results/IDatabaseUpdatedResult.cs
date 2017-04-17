using System.Collections.Generic;

namespace SportsFeed.Services.Results
{
    public interface IDatabaseUpdatedResult
    {
        ISet<int> SportIds { get; }

        ISet<int> EventIds { get; }

        ISet<int> MatchIds { get; }

        ISet<int> BetIds { get; }

        ISet<int> OddIds { get; }
    }
}