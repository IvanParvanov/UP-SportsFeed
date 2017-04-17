using System.Collections.Generic;

using Bytes2you.Validation;

namespace SportsFeed.Services.Results
{
    public class DatabaseCleanedResult : IDatabaseCleanedResult
    {
        public DatabaseCleanedResult()
        {
            this.EventIds = new HashSet<int>();
            this.MatchIds = new HashSet<int>();
            this.BetIds = new HashSet<int>();
        }

        public DatabaseCleanedResult(ISet<int> eventIds, ISet<int> matchIds, ISet<int> betIds)
        {
            Guard.WhenArgument(eventIds, nameof(eventIds)).IsNull().Throw();
            Guard.WhenArgument(matchIds, nameof(matchIds)).IsNull().Throw();
            Guard.WhenArgument(betIds, nameof(betIds)).IsNull().Throw();

            this.EventIds = eventIds;
            this.MatchIds = matchIds;
            this.BetIds = betIds;
        }

        public ISet<int> EventIds { get; }

        public ISet<int> MatchIds { get; }

        public ISet<int> BetIds { get; }
    }
}
