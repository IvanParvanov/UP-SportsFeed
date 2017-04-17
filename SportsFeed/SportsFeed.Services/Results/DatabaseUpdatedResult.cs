using System.Collections.Generic;

using Bytes2you.Validation;

namespace SportsFeed.Services.Results
{
    public class DatabaseUpdatedResult : DatabaseCleanedResult, IDatabaseUpdatedResult
    {
        public DatabaseUpdatedResult()
        {
            this.SportIds = new HashSet<int>();
            this.OddIds = new HashSet<int>();
        }

        public DatabaseUpdatedResult(ISet<int> sportIds,
                                     ISet<int> eventIds,
                                     ISet<int> matchIds,
                                     ISet<int> betIds,
                                     ISet<int> oddIds)
            : base(eventIds, matchIds, betIds)
        {
            Guard.WhenArgument(sportIds, nameof(sportIds)).IsNull().Throw();
            Guard.WhenArgument(oddIds, nameof(oddIds)).IsNull().Throw();

            this.SportIds = sportIds;
            this.OddIds = oddIds;
        }

        public ISet<int> SportIds { get; }

        public ISet<int> OddIds { get; }
    }
}
