using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Bytes2you.Validation;

using SportsFeed.Data.Contracts;
using SportsFeed.Models;
using SportsFeed.Models.Contracts;
using SportsFeed.Services.Contracts;

namespace SportsFeed.Services
{
    public class GroupBySportService : IGroupBySportService
    {
        private readonly ISportsFeedDbContext dbContext;

        public GroupBySportService(ISportsFeedDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, nameof(dbContext)).IsNull().Throw();

            this.dbContext = dbContext;
        }

        public Task<Dictionary<string, IEnumerable<Event>>> GetEventsBySportAsync(ICollection<int> eventIds)
        {
            return this.GetGroupedBySport<Event>(eventIds, ev => ev.Sport, e => e.Sport.Name);
        }

        public Task<Dictionary<string, IEnumerable<Match>>> GetMatchesBySportAsync(ICollection<int> matchIds)
        {
            return this.GetGroupedBySport<Match>(matchIds, ev => ev.Event.Sport, e => e.Event.Sport.Name);
        }

        public Task<Dictionary<string, IEnumerable<Bet>>> GetBetsBySportAsync(ICollection<int> betIds)
        {
            return this.GetGroupedBySport<Bet>(betIds, ev => ev.Match.Event.Sport, e => e.Match.Event.Sport.Name);
        }

        public Task<Dictionary<string, IEnumerable<Odd>>> GetOddsBySportAsync(ICollection<int> oddIds)
        {
            return this.GetGroupedBySport<Odd>(oddIds, ev => ev.Bet.Match.Event.Sport, e => e.Bet.Match.Event.Sport.Name);
        }

        private async Task<Dictionary<string, IEnumerable<T>>> GetGroupedBySport<T>(ICollection<int> ids,
                                                                                    Expression<Func<T, Sport>> includeExpression,
                                                                                    Func<T, string> nameSelector)
            where T : class, IExternalEntity
        {
            var groupedBySport = new Dictionary<string, IEnumerable<T>>();

            var dbEntities = await this.dbContext.Set<T>().Where(x => ids.Contains(x.Id)).Include(includeExpression).ToListAsync();

            var distinctSports = dbEntities.Select(nameSelector).Distinct();
            foreach (var sportName in distinctSports)
            {
                groupedBySport[sportName] = dbEntities.Where(x => nameSelector.Invoke(x) == sportName);
            }

            return groupedBySport;
        }
    }
}
