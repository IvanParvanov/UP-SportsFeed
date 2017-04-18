using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Moq;

using Ninject;

using NUnit.Framework;

using SportsFeed.Models;
using SportsFeed.Services.Contracts;
using SportsFeed.WebClient.Controllers;
using SportsFeed.WebClient.Tests.TestSetups;

using TestStack.FluentMVCTesting;

namespace SportsFeed.WebClient.Tests.WebClient.Controllers
{
    [TestFixture]
    public class HomeControllerTests : HomeControllerTestBase
    {
        [Test]
        public void Ctor_ShouldThrowArgumentNullException_WhenSportsServiceIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(null));
        }

        [Test]
        public void Index_ShouldReturnDefaultPartialView_WhenRequestIsAjax()
        {
            // Arrange
            var sport = string.Empty;
            var sut = this.MockingKernel.Get<HomeController>(AjaxContextName);

            // Act & Assert
            sut.WithCallTo(c => c.Index(sport))
                      .ShouldRenderPartialView("_Index");
        }

        [Test]
        public void Index_ShouldReturnDefaultView_WhenRequestIsNotAjax()
        {
            // Arrange
            var sport = string.Empty;
            var sut = this.MockingKernel.Get<HomeController>(RegularContextName);

            // Act & Assert
            sut.WithCallTo(c => c.Index(sport))
                      .ShouldRenderDefaultView();
        }

        [Test]
        public async Task Index_ShouldCallSportServiceOnceWithCorrectParams()
        {
            // Arrange
            var sport = Guid.NewGuid().ToString();
            var sportService = this.MockingKernel.GetMock<ISportsService>();
            var sut = this.MockingKernel.Get<HomeController>(RegularContextName);

            // Act
            await sut.Index(sport);

            // Assert
            sportService.Verify(s => s.GetSportByNameAsync(sport), Times.Once);
        }

        [Test]
        public void Index_ShouldReturnView_WithReturnValueFromSportsService_WhenRegularContext()
        {
            // Arrange
            var expected = new Sport();
            var sport = Guid.NewGuid().ToString();
            var sportService = this.MockingKernel.GetMock<ISportsService>();
            sportService.Setup(s => s.GetSportByNameAsync(It.IsAny<string>()))
                        .Returns(Task.FromResult(expected));

            var sut = this.MockingKernel.Get<HomeController>(RegularContextName);

            // Act & Assert
            sut.WithCallTo(c => c.Index(sport))
                      .ShouldRenderDefaultView()
                      .WithModel(expected);
        }

        [Test]
        public void Index_ShouldReturnView_WithReturnValueFromSportsService_WhenAjaxContext()
        {
            // Arrange
            var expected = new Sport();
            var sport = Guid.NewGuid().ToString();
            var sportService = this.MockingKernel.GetMock<ISportsService>();
            sportService.Setup(s => s.GetSportByNameAsync(It.IsAny<string>()))
                        .Returns(Task.FromResult(expected));

            var sut = this.MockingKernel.Get<HomeController>(AjaxContextName);

            // Act & Assert
            sut.WithCallTo(c => c.Index(sport))
                      .ShouldRenderPartialView("_Index")
                      .WithModel(expected);
        }

        [Test]
        public void Navbar_ShouldCallSportServiceGetSportsNames()
        {
            // Arrange
            var sut = this.MockingKernel.Get<HomeController>();
            var sportService = this.MockingKernel.GetMock<ISportsService>();

            // Act
            sut.Navbar();

            // Assert
            sportService.Verify(s => s.GetSportsNames(), Times.Once);
        }

        [Test]
        public void Navbar_RenderNavbarPartialViewWithCorrectModel()
        {
            // Arrange
            var expected = new List<string>();
            var sut = this.MockingKernel.Get<HomeController>();
            var sportService = this.MockingKernel.GetMock<ISportsService>();
            sportService.Setup(s => s.GetSportsNames())
                        .Returns(expected);

            // Act & Assert
            sut.WithCallTo(s => s.Navbar())
               .ShouldRenderPartialView("_Navbar")
               .WithModel(expected);
        }
    }
}
