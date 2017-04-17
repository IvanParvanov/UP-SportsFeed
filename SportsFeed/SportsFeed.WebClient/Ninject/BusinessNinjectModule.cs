using AutoMapper;

using FluentScheduler;

using Ninject.Extensions.Factory;
using Ninject.Modules;

using SportsFeed.BackgroundWorkers.Helpers;
using SportsFeed.BackgroundWorkers.Helpers.Contracts;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs;
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

            this.Bind<IJob>()
                .To<UpdateDatabaseJob>()
                .InSingletonScope()
                .Named(typeof(UpdateDatabaseJob).Name);

            this.Bind<UpdateDatabaseJob>()
                .ToSelf()
                .Named(typeof(UpdateDatabaseJob).Name);

            this.Rebind<IBetInformationService>()
                .To<VitalBetInformationService>()
                .InSingletonScope();

            this.Bind<IDbUpdatedResultFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}