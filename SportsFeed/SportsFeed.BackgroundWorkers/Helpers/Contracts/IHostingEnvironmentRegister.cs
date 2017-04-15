using System.Web.Hosting;

namespace SportsFeed.BackgroundWorkers.Helpers.Contracts
{
    public interface IHostingEnvironmentRegister
    {
        void Register(IRegisteredObject obj);

        void Unregister(IRegisteredObject obj);
    }
}
