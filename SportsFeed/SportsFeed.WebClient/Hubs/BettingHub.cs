using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bytes2you.Validation;

using Microsoft.AspNet.SignalR;

using SportsFeed.BackgroundWorkers.EventArgs;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts;
using SportsFeed.Services.Contracts;
using SportsFeed.WebClient.Hubs.Clients;
using SportsFeed.WebClient.Models.Dtos;

namespace SportsFeed.WebClient.Hubs
{
    public class BettingHub : Hub<IDbUpdatesNotifiedClient>
    {
        private readonly IGroupBySportService dataService;

        public BettingHub(INotifyDatabaseUpdated notifyUpdated, INotifyDatabaseCleanup notifyCleanup, IGroupBySportService dataService)
        {
            Guard.WhenArgument(notifyUpdated, nameof(notifyUpdated)).IsNull().Throw();
            Guard.WhenArgument(notifyCleanup, nameof(notifyCleanup)).IsNull().Throw();
            Guard.WhenArgument(dataService, nameof(dataService)).IsNull().Throw();

            this.dataService = dataService;

            if (!notifyUpdated.HasSubscribers())
            {
                notifyUpdated.DatabaseUpdated += this.NotifierOnDatabaseUpdated;
            }

            if (!notifyCleanup.HasSubscribers())
            {
                notifyCleanup.DatabaseCleaned += this.NotifyCleanupOnDatabaseCleaned;
            }
        }

        public Task JoinGroup(string sportName)
        {
            return this.Groups.Add(this.Context.ConnectionId, sportName);
        }

        public Task LeaveGroup(string sportName)
        {
            return this.Groups.Remove(this.Context.ConnectionId, sportName);
        }

        private async void NotifyCleanupOnDatabaseCleaned(object sender, DatabaseCleanedEventArgs args)
        {
            var events = await this.dataService.GetEventsBySportAsync(args.Changes.EventIds);
            var matches = await this.dataService.GetMatchesBySportAsync(args.Changes.MatchIds);
            var bets = await this.dataService.GetBetsBySportAsync(args.Changes.BetIds);

            var allSports = events.Keys.Concat(matches.Keys)
                                  .Concat(bets.Keys)
                                  .Distinct()
                                  .ToList();

            foreach (var sport in allSports)
            {
                var dto = new DbChangeDto()
                {
                    Events = GetValue(events, sport),
                    Matches = GetValue(matches, sport),
                    Bets = GetValue(bets, sport),
                };

                this.Clients.Group(sport).SendDeleteData(dto);
            }
        }

        private async void NotifierOnDatabaseUpdated(object sender, DatabaseUpdatedEventArgs args)
        {
            var events = await this.dataService.GetEventsBySportAsync(args.Changes.EventIds);
            var matches = await this.dataService.GetMatchesBySportAsync(args.Changes.MatchIds);
            var bets = await this.dataService.GetBetsBySportAsync(args.Changes.BetIds);
            var odds = await this.dataService.GetOddsBySportAsync(args.Changes.OddIds);

            var allSports = events.Keys.Concat(matches.Keys)
                                  .Concat(bets.Keys)
                                  .Concat(odds.Keys)
                                  .Distinct()
                                  .ToList();

            foreach (var sport in allSports)
            {
                var dto = new DbChangeDto()
                          {
                              Events = GetValue(events, sport),
                              Matches = GetValue(matches, sport),
                              Bets = GetValue(bets, sport),
                              Odds = GetValue(odds, sport)
                          };

                this.Clients.Group(sport).SendUpdateData(dto);
            }
        }

        private static IEnumerable<T> GetValue<T>(IDictionary<string, IEnumerable<T>> elements, string key)
        {
            IEnumerable<T> found;
            elements.TryGetValue(key, out found);

            return found;
        }
    }
}
