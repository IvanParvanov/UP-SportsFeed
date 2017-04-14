using Bytes2you.Validation;

using FluentScheduler;

using Ninject;

namespace SportsFeed.WebClient.Ninject.Resolvers
{
    public class NinjectFluentSchedulerDependencyResolver : IJobFactory
    {
        private readonly IKernel kernel;

        public NinjectFluentSchedulerDependencyResolver(IKernel kernel)
        {
            Guard.WhenArgument(kernel, nameof(kernel)).IsNull().Throw();

            this.kernel = kernel;
        }

        public IJob GetJobInstance<T>() where T : IJob
        {
            var serviceType = typeof(T);
            var instance = (T)this.kernel.Get(serviceType, serviceType.Name);

            return instance;
        }
    }
}
