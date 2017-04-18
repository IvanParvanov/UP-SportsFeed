using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

using Moq;

using Ninject;
using Ninject.MockingKernel;

using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts;
using SportsFeed.Services.Contracts;
using SportsFeed.WebClient.Hubs;
using SportsFeed.WebClient.Tests.TestSetups.Base;

namespace SportsFeed.WebClient.Tests.TestSetups
{
    public class BettingHubTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.MockingKernel.Bind<INotifyDatabaseUpdated>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<INotifyDatabaseCleanup>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IGroupBySportService>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IGroupManager>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<HubCallerContext>().ToMock().InSingletonScope();

            this.MockingKernel.Bind<BettingHub>()
                .ToSelf()
                .Named(RegularContextName)
                .BindingConfiguration.IsImplicit = true;

            this.MockingKernel.Bind<BettingHub>()
                .ToMethod(ctx =>
                          {
                              var sut = this.MockingKernel.Get<BettingHub>(RegularContextName);
                              var groups = this.MockingKernel.GetMock<IGroupManager>();
                              var context = this.MockingKernel.GetMock<HubCallerContext>();

                              sut.Groups = groups.Object;
                              sut.Context = context.Object;

                              return sut;
                          });
        }
    }
}
