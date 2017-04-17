using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;

using Bytes2you.Validation;

using SportsFeed.Data.Contracts;
using SportsFeed.Services.Contracts;
using SportsFeed.Services.Factories;
using SportsFeed.Services.Results;

namespace SportsFeed.Services
{
    public class DbSyncService : IDbSyncService
    {
        private readonly ISportsFeedDbContext dbContext;
        private readonly IBetInformationService betService;
        private readonly IDbUpdatedResultFactory resultFactory;

        public DbSyncService(ISportsFeedDbContext dbContext, IBetInformationService betService, IDbUpdatedResultFactory resultFactory)
        {
            Guard.WhenArgument(dbContext, nameof(dbContext)).IsNull().Throw();
            Guard.WhenArgument(betService, nameof(betService)).IsNull().Throw();
            Guard.WhenArgument(resultFactory, nameof(resultFactory)).IsNull().Throw();

            this.dbContext = dbContext;
            this.betService = betService;
            this.resultFactory = resultFactory;
        }

        public virtual IDatabaseUpdatedResult SyncDatabase()
        {
            var st = new Stopwatch();
            st.Start();

            var databaseIsEmpty = !this.dbContext.Sports.Any();

            var webSports = this.betService.GetData();

            var changedEvents = webSports.SelectMany(s => s.Events).ToArray();
            var changedMatches = changedEvents.SelectMany(e => e.Matches).ToArray();
            var changedBets = changedMatches.SelectMany(m => m.Bets).ToArray();
            var changedOdds = changedBets.SelectMany(m => m.Odds).ToArray();

            var changedSports = webSports.Except(this.dbContext.Sports.ToArray()).ToArray();
            changedEvents = changedEvents.Except(this.dbContext.Events.ToArray()).ToArray();
            changedMatches = changedMatches.Except(this.dbContext.Matches.ToArray()).ToArray();
            changedBets = changedBets.Except(this.dbContext.Bets.ToArray()).ToArray();
            changedOdds = changedOdds.Except(this.dbContext.Odds.ToArray()).ToArray();

            this.dbContext.Odds.AddOrUpdate(changedOdds);
            this.dbContext.Bets.AddOrUpdate(changedBets);
            this.dbContext.Matches.AddOrUpdate(changedMatches);
            this.dbContext.Events.AddOrUpdate(changedEvents);
            this.dbContext.Sports.AddOrUpdate(changedSports);

            this.dbContext.SaveChanges();

            if (databaseIsEmpty)
            {
                return this.resultFactory.CreateDatabaseUpdatedResult();
            }

            var sportIds = new HashSet<int>(changedSports.Select(s => s.Id));
            var eventIds = new HashSet<int>(changedEvents.Select(s => s.Id));
            var matchIds = new HashSet<int>(changedMatches.Select(s => s.Id));
            var betIds = new HashSet<int>(changedBets.Select(s => s.Id));
            var oddIds = new HashSet<int>(changedOdds.Select(s => s.Id));

            var result = this.resultFactory.CreateDatabaseUpdatedResult(sportIds, eventIds, matchIds, betIds, oddIds);

            st.Stop();
            var a = st.Elapsed;
            return result;
        }
    }
}
