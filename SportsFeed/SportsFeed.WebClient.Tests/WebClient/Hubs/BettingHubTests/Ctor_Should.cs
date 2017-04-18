using System;

using Moq;

using NUnit.Framework;

using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts;
using SportsFeed.Services.Contracts;
using SportsFeed.WebClient.Hubs;

namespace SportsFeed.WebClient.Tests.WebClient.Hubs.BettingHubTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Throw_ArgumentNullException_WhenINotifyDatabaseUpdatedIsNull()
        {
            // Arrange
            var notifyCleanup = new Mock<INotifyDatabaseCleanup>();
            var dataService = new Mock<IGroupBySportService>();

            // Assert & Assert
            Assert.Throws<ArgumentNullException>(() => new BettingHub(null, notifyCleanup.Object, dataService.Object));
        }

        [Test]
        public void Throw_ArgumentNullException_WhenINotifyDatabaseCleanupIsNull()
        {
            // Arrange
            var notifyUpdate = new Mock<INotifyDatabaseUpdated>();
            var dataService = new Mock<IGroupBySportService>();

            // Assert & Assert
            Assert.Throws<ArgumentNullException>(() => new BettingHub(notifyUpdate.Object, null, dataService.Object));
        }

        [Test]
        public void Throw_ArgumentNullException_WhenDataServiceIsNull()
        {
            // Arrange
            var notifyCleanup = new Mock<INotifyDatabaseCleanup>();
            var notifyUpdate = new Mock<INotifyDatabaseUpdated>();

            // Assert & Assert
            Assert.Throws<ArgumentNullException>(() => new BettingHub(notifyUpdate.Object, notifyCleanup.Object, null));
        }
    }
}
