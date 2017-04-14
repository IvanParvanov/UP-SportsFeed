using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using FluentScheduler;

using Microsoft.AspNet.SignalR;

using SportsFeed.Data;
using SportsFeed.WebClient.App_Start;
using SportsFeed.WebClient.Ninject.Resolvers;
using SportsFeed.WebClient.ScheduledJobs;

namespace SportsFeed.WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(NinjectWebCommon.Kernel);

            AutoMapConfig.RegisterMappings();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportsFeedDbContext, Data.Migrations.Configuration>());

            JobManager.JobFactory = new NinjectFluentSchedulerDependencyResolver(NinjectWebCommon.Kernel);
            JobManager.Initialize(new UpdateDatabaseRegistry());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_PreSendRequestHeaders()
        {
            this.Response.Headers.Remove("Server");
            this.Response.Headers.Remove("X-AspNet-Version");
            this.Response.Headers.Remove("X-AspNetMvc-Version");
        }
    }
}
