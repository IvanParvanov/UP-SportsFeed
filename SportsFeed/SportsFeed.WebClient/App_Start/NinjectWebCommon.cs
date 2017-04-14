using System;
using System.Web;

using Bytes2you.Validation;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;

using SportsFeed.WebClient.Ninject;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SportsFeed.WebClient.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(SportsFeed.WebClient.App_Start.NinjectWebCommon), "Stop")]

namespace SportsFeed.WebClient.App_Start
{
    public static class NinjectWebCommon
    {
        private static IKernel kernel;

        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static IKernel Kernel
        {
            get
            {
                return kernel;
            }

            private set
            {
                Guard.WhenArgument(value, nameof(Kernel)).IsNull().Throw();

                kernel = value;
            }
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x => x.FromAssembliesMatching("SportsFeed.*")
                              .SelectAllClasses()
                              .BindDefaultInterfaces());

            kernel.Load(new DataNinjectModule());
            kernel.Load(new BusinessNinjectModule());
        }        
    }
}
