using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;

using Bytes2you.Validation;

using SportsFeed.Data.Contracts;
using SportsFeed.Models;
using SportsFeed.Models.Contracts;
using SportsFeed.Services.Contracts;

namespace SportsFeed.Services
{
    public class DbSyncService : IDbSyncService
    {
        private readonly ISportsFeedDbContext dbContext;
        private readonly IBetInformationService betService;
        private ISet<Sport> sports;
        private ISet<Event> events;
        private ISet<Match> matches;
        private ISet<Bet> bets;
        private ISet<Odd> odds;

        public DbSyncService(ISportsFeedDbContext dbContext, IBetInformationService betService)
        {
            Guard.WhenArgument(dbContext, nameof(dbContext)).IsNull().Throw();
            Guard.WhenArgument(betService, nameof(betService)).IsNull().Throw();

            this.dbContext = dbContext;
            this.betService = betService;

            this.UpdateInMemoryCollections();
        }

        public IEnumerable<IExternalEntity> SyncDatabase()
        {
            var st = new Stopwatch();
            var webSports = this.betService.GetData();
            st.Start();

            var newEvents = webSports.SelectMany(s => s.Events).ToArray();
            var newMatches = newEvents.SelectMany(e => e.Matches).ToArray();
            var newBets = newMatches.SelectMany(m => m.Bets).ToArray();
            var newOdds = newBets.SelectMany(m => m.Odds).ToArray();

            var newSports = webSports.Except(this.sports).ToArray();
            newEvents = newEvents.Except(this.events).ToArray();
            newMatches = newMatches.Except(this.matches).ToArray();
            newBets = newBets.Except(this.bets).ToArray();
            newOdds = newOdds.Except(this.odds).ToArray();

            this.dbContext.Odds.AddOrUpdate(newOdds);
            this.dbContext.Bets.AddOrUpdate(newBets);
            this.dbContext.Matches.AddOrUpdate(newMatches);
            this.dbContext.Events.AddOrUpdate(newEvents);
            this.dbContext.Sports.AddOrUpdate(newSports);

            this.dbContext.SaveChanges();
            st.Stop();
            var a = st.Elapsed;

            this.UpdateInMemoryCollections();
            
            return webSports;
        }

        private void UpdateInMemoryCollections()
        {
            var sports = this.dbContext.Sports.ToList();
            this.sports = new HashSet<Sport>(sports);

            var events = this.dbContext.Events.ToList();
            this.events = new HashSet<Event>(events);

            var matches = this.dbContext.Matches.ToList();
            this.matches = new HashSet<Match>(matches);

            var bets = this.dbContext.Bets.ToList();
            this.bets = new HashSet<Bet>(bets);

            var odds = this.dbContext.Odds.ToList();
            this.odds = new HashSet<Odd>(odds);
        }
    }
}
