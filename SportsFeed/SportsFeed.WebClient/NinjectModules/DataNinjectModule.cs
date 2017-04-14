using Ninject.Activation;
using Ninject.Modules;
using Ninject.Web.Common;

using SportsFeed.Data;

namespace SportsFeed.WebClient.NinjectModules
{
    public class DataNinjectModule: NinjectModule
    {
        public override void Load()
        {
            this.Rebind<ISportsFeedDbContext>().To<SportsFeedDbContext>().InRequestScope();

            this.Bind<ISaveable>().To<SportsFeedDbContext>().InRequestScope();
        }

        private static bool HasAncestorAssignableFrom<T>(IRequest request)
        {
            while ( true )
            {
                if ( request == null )
                {
                    return false;
                }

                if ( typeof(T).IsAssignableFrom(request.Service) )
                {
                    return true;
                }

                request = request.ParentRequest;
            }
        }

        private static bool DoesNotHaveAncestorAssignableFrom<T>(IRequest request)
        {
            return !HasAncestorAssignableFrom<T>(request);
        }
    }
}