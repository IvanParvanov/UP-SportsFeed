using AutoMapper;

using FluentScheduler;

using Ninject.Modules;

using SportsFeed.WebClient.ScheduledJobs.Helpers;
using SportsFeed.WebClient.ScheduledJobs.Helpers.Contracts;
using SportsFeed.WebClient.ScheduledJobs.Jobs;

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