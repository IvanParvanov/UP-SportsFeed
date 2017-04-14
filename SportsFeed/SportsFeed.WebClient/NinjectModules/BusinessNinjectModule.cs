using AutoMapper;

using Ninject.Modules;

namespace SportsFeed.WebClient.NinjectModules
{
    public class BusinessNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapper>()
                .ToMethod(ctx => Mapper.Instance)
                .InSingletonScope();
        }
    }
}