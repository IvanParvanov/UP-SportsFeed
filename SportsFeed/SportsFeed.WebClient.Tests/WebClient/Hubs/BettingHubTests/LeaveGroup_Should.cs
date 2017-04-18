using System;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

using Moq;

using Ninject;

using NUnit.Framework;

using SportsFeed.WebClient.Hubs;
using SportsFeed.WebClient.Tests.TestSetups;

namespace SportsFeed.WebClient.Tests.WebClient.Hubs.BettingHubTests
{
    [TestFixture]
    public class LeaveGroup_Should : BettingHubTestBase
    {
        [Test]
        public async Task Add_CurrentConnectionId_ToCorrectGroup()
        {
            // Arrange
            var expectedSport = Guid.NewGuid().ToString();
            var expectedConnectionId = Guid.NewGuid().ToString();
            var sut = this.MockingKernel.Get<BettingHub>();
            var groups = this.MockingKernel.GetMock<IGroupManager>();
            var context = this.MockingKernel.GetMock<HubCallerContext>();
            context.Setup(c => c.ConnectionId)
                   .Returns(expectedConnectionId);

            // Act
            await sut.LeaveGroup(expectedSport);

            groups.Verify(g => g.Remove(expectedConnectionId, expectedSport), Times.Once);

        }
    }
}
