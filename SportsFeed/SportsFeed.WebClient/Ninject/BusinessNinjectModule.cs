using AutoMapper;

using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;

using SportsFeed.BackgroundWorkers.Helpers;
using SportsFeed.BackgroundWorkers.Helpers.Contracts;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts;
using SportsFeed.Services;
using SportsFeed.Services.Contracts;
using SportsFeed.Services.Factories;

namespace SportsFeed.WebClient.Ninject
{
    public class BusinessNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapper>()
                .ToMethod(ctx => Mapper.Instance)
                .InSingletonScope();

            this.Rebind<IHostingEnvironmentRegister>()
                .To<HostingEnvironmentRegister>()
                .InSingletonScope();

            this.Bind<UpdateDatabaseJob>()
                .ToSelf()
                .InSingletonScope();

            this.Bind<INotifyDatabaseUpdated>()
                .ToMethod(ctx =>
                          {
                              var job = ctx.Kernel.Get<UpdateDatabaseJob>();

                              return job;
                          });

            this.Rebind<IBetInformationService>()
                .To<VitalBetInformationService>()
                .InSingletonScope();

            this.Bind<IDbUpdatedResultFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}