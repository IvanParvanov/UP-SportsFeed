using System.Web.Hosting;

using SportsFeed.WebClient.ScheduledJobs.Helpers.Contracts;

namespace SportsFeed.WebClient.ScheduledJobs.Helpers
{
    public class HostingEnvironmentRegister : IHostingEnvironmentRegister
    {
        public void Register(IRegisteredObject obj)
        {
            HostingEnvironment.RegisterObject(obj);
        }

        public void Unregister(IRegisteredObject obj)
        {
            HostingEnvironment.UnregisterObject(obj);
        }
    }
}