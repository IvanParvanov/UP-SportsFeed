using System.Web.Hosting;

using SportsFeed.BackgroundWorkers.Helpers.Contracts;

namespace SportsFeed.BackgroundWorkers.Helpers
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