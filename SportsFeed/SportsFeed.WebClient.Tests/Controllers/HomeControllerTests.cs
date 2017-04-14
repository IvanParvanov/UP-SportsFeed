using NUnit.Framework;

using SportsFeed.WebClient.Controllers;

using TestStack.FluentMVCTesting;

namespace SportsFeed.WebClient.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act & Assert
            controller.WithCallTo(c => c.Index())
                      .ShouldRenderDefaultView();
        }
    }
}
