using System;
using System.Web.Hosting;

using Bytes2you.Validation;

using FluentScheduler;

using SportsFeed.Data.Contracts;
using SportsFeed.WebClient.ScheduledJobs.Helpers.Contracts;

namespace SportsFeed.WebClient.ScheduledJobs.Jobs
{
    public class UpdateDatabaseJob : IJob, IRegisteredObject
    {
        public event EventHandler DatabaseUpdatedTick;

        private readonly object locker = new object();

        private readonly ISportsFeedDbContext dbContext;

        private readonly IHostingEnvironmentRegister hostingEnvironment;

        private bool shuttingDown;

        public UpdateDatabaseJob(ISportsFeedDbContext dbContext, IHostingEnvironmentRegister hostingEnvironment)
        {
            Guard.WhenArgument(dbContext, nameof(dbContext)).IsNull().Throw();
            Guard.WhenArgument(hostingEnvironment, nameof(hostingEnvironment)).IsNull().Throw();

            this.dbContext = dbContext;
            this.hostingEnvironment = hostingEnvironment;
            this.hostingEnvironment.Register(this);
        }

        public void Execute()
        {
            lock (this.locker)
            {
                if (this.shuttingDown)
                {
                    return;
                }

                //var client = new System.Net.WebClient();
                //var text = client.DownloadString("http://vitalbet.net/sportxml");
                //var a = XDocument.Parse(text);

                this.DatabaseUpdatedTick?.Invoke(this, null);
            }
        }

        public void Stop(bool immediate)
        {
            lock (this.locker)
            {
                this.shuttingDown = true;
            }

            this.dbContext.Dispose();
            this.hostingEnvironment.Unregister(this);
        }
    }
}
