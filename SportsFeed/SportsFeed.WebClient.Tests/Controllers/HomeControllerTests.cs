using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Moq;

using NUnit.Framework;

using SportsFeed.Services.Contracts;
using SportsFeed.WebClient.Controllers;

using TestStack.FluentMVCTesting;

namespace SportsFeed.WebClient.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void Index_ShouldReturnDefaultPartialView_WhenRequestIsAjax()
        {
            // Arrange
            var sport = string.Empty;
            var sportsService = new Mock<ISportsService>();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                                                     new WebHeaderCollection
                                                     {
                                                                           { "X-Requested-With", "XMLHttpRequest" }
                                                     });

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var sut = new HomeController(sportsService.Object);
            sut.ControllerContext = new ControllerContext(context.Object, new RouteData(), sut);

            // Act & Assert
            sut.WithCallTo(c => c.Index(sport))
                      .ShouldRenderPartialView("_Index");
        }

        [Test]
        public void Index_ShouldReturnDefaultView_WhenRequestIsNotAjax()
        {
            // Arrange
            var sport = string.Empty;
            var sportsService = new Mock<ISportsService>();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                                                     new WebHeaderCollection());
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var sut = new HomeController(sportsService.Object);
            sut.ControllerContext = new ControllerContext(context.Object, new RouteData(), sut);

            // Act & Assert
            sut.WithCallTo(c => c.Index(sport))
                      .ShouldRenderDefaultView();
        }
    }
}
