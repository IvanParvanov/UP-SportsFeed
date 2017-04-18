using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Hubs;

using Moq;

using Ninject;

using NUnit.Framework;

using Ploeh.AutoFixture;

using SportsFeed.BackgroundWorkers.EventArgs;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts;
using SportsFeed.Models;
using SportsFeed.Services.Contracts;
using SportsFeed.Services.Results;
using SportsFeed.WebClient.Hubs;
using SportsFeed.WebClient.Hubs.Clients;
using SportsFeed.WebClient.Models.Dtos;
using SportsFeed.WebClient.Tests.TestSetups;

using Match = SportsFeed.Models.Match;

namespace SportsFeed.WebClient.Tests.WebClient.Hubs.BettingHubTests
{
    [TestFixture]
    public class NotifyCleanupOnDatabaseCleaned_Should : BettingHubTestBase
    {
        public override void Init()
        {
            base.Init();

            this.MockingKernel.Rebind<IGroupBySportService>()
                .ToMethod(ctx =>
                          {
                              var groupService = new Mock<IGroupBySportService>();
                              groupService.Setup(g => g.GetEventsBySportAsync(It.IsAny<ISet<int>>()))
                                          .Returns(Task.FromResult(GetMockedDictionary<Event>()));

                              groupService.Setup(g => g.GetBetsBySportAsync(It.IsAny<ISet<int>>()))
                                          .Returns(Task.FromResult(GetMockedDictionary<Bet>()));

                              groupService.Setup(g => g.GetMatchesBySportAsync(It.IsAny<ISet<int>>()))
                                          .Returns(Task.FromResult(GetMockedDictionary<Match>()));

                              return groupService.Object;
                          })
                .InSingletonScope();
        }

        [Test]
        public void Call_DataService_GetMethodsOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BettingHub>();
            var notifyCleaned = this.MockingKernel.GetMock<INotifyDatabaseCleanup>();
            var dbUpdateResult = GetMockedUpdateData();
            var dataService = this.MockingKernel.GetMock<IGroupBySportService>();
            var args = new DatabaseCleanedEventArgs(dbUpdateResult);

            // Act
            notifyCleaned.Raise(x => x.DatabaseCleaned += null, notifyCleaned.Object, args);

            // Assert
            dataService.Verify(s => s.GetEventsBySportAsync(args.Changes.EventIds), Times.Once);
            dataService.Verify(s => s.GetBetsBySportAsync(args.Changes.BetIds), Times.Once);
            dataService.Verify(s => s.GetMatchesBySportAsync(args.Changes.MatchIds), Times.Once);
        }

        [Test]
        public async Task Call_ClientsGroupAndSendDeleteDataMethods_OnceForEachDistinctSport()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BettingHub>();
            var mockedConnectedClients = new Mock<IHubCallerConnectionContext<IDbUpdatesNotifiedClient>>();
            var mockedFoundClients = new Mock<IDbUpdatesNotifiedClient>();
            mockedConnectedClients.Setup(c => c.Group(It.IsAny<string>()))
                                  .Returns(mockedFoundClients.Object);
            sut.Clients = mockedConnectedClients.Object;

            var notifyCleaned = this.MockingKernel.GetMock<INotifyDatabaseCleanup>();

            var groupService = this.MockingKernel.GetMock<IGroupBySportService>();
            var events = await groupService.Object.GetEventsBySportAsync(null);
            var bets = await groupService.Object.GetBetsBySportAsync(null);
            var matches = await groupService.Object.GetMatchesBySportAsync(null);

            var allSports = events.Keys.Concat(matches.Keys).Concat(bets.Keys).Distinct().ToList();
            var sportsCount = allSports.Count;

            var dbUpdateResult = GetMockedUpdateData();
            var args = new DatabaseCleanedEventArgs(dbUpdateResult);

            // Act
            notifyCleaned.Raise(x => x.DatabaseCleaned += null, notifyCleaned.Object, args);

            // Assert
            foreach (var expectedSport in allSports)
            {
                mockedConnectedClients.Verify(c => c.Group(expectedSport), Times.Once);
            }

            mockedFoundClients.Verify(c => c.SendDeleteData(It.IsAny<DbChangeDto>()), Times.Exactly(sportsCount));
        }

        private static Dictionary<string, IEnumerable<T>> GetMockedDictionary<T>()
        {
            var mockData = new Dictionary<string, IEnumerable<T>>();
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                   .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Register<IEqualityComparer<T>>(() => EqualityComparer<T>.Default);

            for (var i = 0; i < 5; i++)
            {
                var sportName = Guid.NewGuid().ToString();
                mockData[sportName] = fixture.Create<IEnumerable<T>>();
            }

            return mockData;
        }

        private static IDatabaseCleanedResult GetMockedUpdateData()
        {
            var expectedEventIds = new HashSet<int>();
            var expectedMatchIds = new HashSet<int>();
            var expectedBetIds = new HashSet<int>();
            var result = new Mock<IDatabaseCleanedResult>();
            result.Setup(r => r.BetIds)
                  .Returns(expectedBetIds);
            result.Setup(r => r.MatchIds)
                  .Returns(expectedMatchIds);
            result.Setup(r => r.EventIds)
                  .Returns(expectedEventIds);

            return result.Object;
        }
    }
}
