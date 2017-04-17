using System.Linq;
using System.Threading.Tasks;

using Bytes2you.Validation;

using Microsoft.AspNet.SignalR;

using SportsFeed.BackgroundWorkers.EventArgs;
using SportsFeed.BackgroundWorkers.ScheduledJobs.Jobs.Contracts;

namespace SportsFeed.WebClient.Hubs
{
    public class BettingHub : Hub
    {
        private readonly INotifyDatabaseUpdated notify;

        public BettingHub(INotifyDatabaseUpdated notifier)
        {
            Guard.WhenArgument(notifier, nameof(notifier)).IsNull().Throw();

            this.notify = notifier;
            this.notify.DatabaseUpdated += this.NotifierOnDatabaseUpdated;
        }

        public Task JoinGroup(string sportName)
        {
            return this.Groups.Add(this.Context.ConnectionId, sportName);
        }

        public Task LeaveGroup(string sportName)
        {
            return this.Groups.Remove(this.Context.ConnectionId, sportName);
        }

        private void NotifierOnDatabaseUpdated(object sender, DatabaseUpdatedEventArgs databaseUpdatedEventArgs)
        {
            var a = databaseUpdatedEventArgs;

            this.Clients.Group("Soccer").ShowNotification(a);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    this.notify.DatabaseUpdated -= this.NotifierOnDatabaseUpdated;

        //    base.Dispose(disposing);
        //}
    }
}