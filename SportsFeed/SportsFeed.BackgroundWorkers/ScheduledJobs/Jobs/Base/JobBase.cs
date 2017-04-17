using System.Web.Hosting;

using Bytes2you.Validation;

using FluentScheduler;

using SportsFeed.BackgroundWorkers.Helpers.Contracts;

namespace SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Base
{
    public abstract class JobBase : IJob, IRegisteredObject
    {
        protected readonly object Locker = new object();

        protected bool ShuttingDown;

        private readonly IHostingEnvironmentRegister hostingEnvironment;

        protected JobBase(IHostingEnvironmentRegister hostingEnvironment)
        {
            Guard.WhenArgument(hostingEnvironment, nameof(hostingEnvironment)).IsNull().Throw();

            this.hostingEnvironment = hostingEnvironment;
            this.hostingEnvironment.Register(this);
        }

        public abstract void Execute();

        public void Stop(bool immediate)
        {
            lock (this.Locker)
            {
                this.ShuttingDown = true;
            }

            this.hostingEnvironment.Unregister(this);
        }
    }
}
