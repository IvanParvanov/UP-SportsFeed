using System.Collections.Generic;

using Bytes2you.Validation;

namespace SportsFeed.Services.Results
{
    public class DatabaseUpdatedResult : IDatabaseUpdatedResult
    {
        public DatabaseUpdatedResult()
        {
            this.SportIds = new HashSet<int>();
            this.EventIds = new HashSet<int>();
            this.MatchIds = new HashSet<int>();
            this.BetIds = new HashSet<int>();
            this.OddIds = new HashSet<int>();
        }

        public DatabaseUpdatedResult(
            ISet<int> sportIds,
            ISet<int> eventIds,
            ISet<int> matchIds,
            ISet<int> betIds,
            ISet<int> oddIds)
        {
            Guard.WhenArgument(sportIds, nameof(sportIds)).IsNull().Throw();
            Guard.WhenArgument(eventIds, nameof(eventIds)).IsNull().Throw();
            Guard.WhenArgument(matchIds, nameof(matchIds)).IsNull().Throw();
            Guard.WhenArgument(betIds, nameof(betIds)).IsNull().Throw();
            Guard.WhenArgument(oddIds, nameof(oddIds)).IsNull().Throw();

            this.SportIds = sportIds;
            this.EventIds = eventIds;
            this.MatchIds = matchIds;
            this.BetIds = betIds;
            this.OddIds = oddIds;
        }

        public ISet<int> SportIds { get; }

        public ISet<int> EventIds { get; }

        public ISet<int> MatchIds { get; }

        public ISet<int> BetIds { get; }

        public ISet<int> OddIds { get; }
    }
}
