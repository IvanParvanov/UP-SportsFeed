using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Moq;

using Ninject;
using Ninject.MockingKernel;

using SportsFeed.Services.Contracts;
using SportsFeed.WebClient.Controllers;
using SportsFeed.WebClient.Tests.TestSetups.Base;

namespace SportsFeed.WebClient.Tests.TestSetups
{
    public class HomeControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.MockingKernel.Bind<ISportsService>().ToMock().InSingletonScope();

            this.MockingKernel.Bind<HomeController>()
                .ToSelf();

            this.MockingKernel.Bind<HomeController>()
                .ToMethod(ctx =>
                          {
                              var sut = ctx.Kernel.Get<HomeController>();
                              var httpContext = ctx.Kernel.Get<HttpContextBase>(RegularContextName);
                              sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                              return sut;
                          })
                .Named(RegularContextName)
                .BindingConfiguration.IsImplicit = true;

            this.MockingKernel.Bind<HomeController>()
                .ToMethod(ctx =>
                {
                    var sut = ctx.Kernel.Get<HomeController>();
                    var httpContext = ctx.Kernel.Get<HttpContextBase>(AjaxContextName);
                    sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                    return sut;
                })
                .Named(AjaxContextName)
                .BindingConfiguration.IsImplicit = true;

            this.MockingKernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                {
                    var request = new Mock<HttpRequestBase>();
                    request.SetupGet(x => x.Headers).Returns(
                                                             new WebHeaderCollection
                                                             {
                                                                           { "X-Requested-With", "XMLHttpRequest" }
                                                             });

                    var context = new Mock<HttpContextBase>();
                    context.SetupGet(x => x.Request).Returns(request.Object);

                    return context.Object;
                })
                .InSingletonScope()
                .Named(AjaxContextName);

            this.MockingKernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                {
                    var request = new Mock<HttpRequestBase>();
                    request.SetupGet(x => x.Headers).Returns(new WebHeaderCollection());

                    var context = new Mock<HttpContextBase>();
                    context.SetupGet(x => x.Request).Returns(request.Object);

                    return context.Object;
                })
                .InSingletonScope()
                .Named(RegularContextName);
        }
    }
}
