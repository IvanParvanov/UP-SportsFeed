using AutoMapper;

using FluentScheduler;

using Ninject.Modules;

using SportsFeed.BackgroundWorkers.Helpers;
using SportsFeed.BackgroundWorkers.Helpers.Contracts;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs;

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
        }
    }
}