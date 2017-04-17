using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Bytes2you.Validation;

using SportsFeed.Data.Contracts;
using SportsFeed.Models;
using SportsFeed.Models.Contracts;
using SportsFeed.Services.Contracts;
using SportsFeed.Services.Factories;
using SportsFeed.Services.Results;

namespace SportsFeed.Services
{
    public class DbCleanupService : IDbCleanupService
    {
        private readonly IDbUpdatedResultFactory resultFactory;
        private readonly ISportsFeedDbContext dbContext;

        public DbCleanupService(IDbUpdatedResultFactory resultFactory, ISportsFeedDbContext dbContext)
        {
            Guard.WhenArgument(resultFactory, nameof(resultFactory)).IsNull().Throw();
            Guard.WhenArgument(dbContext, nameof(dbContext)).IsNull().Throw();

            this.resultFactory = resultFactory;
            this.dbContext = dbContext;
        }

        public IDatabaseCleanedResult CleanStaleData()
        {
            var removedBets = this.RemoveEntities<Bet>(b => b.Odds.Count == 0);

            var now = DateTime.UtcNow;
            var tommorow = now.AddDays(1);

            var removedMatches = this.RemoveEntities<Match>(m => m.Bets.Count == 0
                                                                 || m.StartDate < now
                                                                 || m.StartDate > tommorow);

            var removedEvents = this.RemoveEntities<Event>(e => e.Matches.Count == 0);

            return this.resultFactory.CreateDatabaseCleanedResult(removedEvents,
                                                                  removedMatches,
                                                                  removedBets);
        }

        private ICollection<int> RemoveEntities<T>(Expression<Func<T, bool>> removePredicate)
            where T : class, IIdentifiable
        {
            var entities = this.dbContext.Set<T>().Where(removePredicate).ToList();

            foreach (var entity in entities)
            {
                this.dbContext.Set<T>().Remove(entity);
            }

            this.dbContext.SaveChanges();

            var ids = new HashSet<int>(entities.Select(e => e.Id));

            return ids;
        }
    }
}
