using Bytes2you.Validation;

using SportsFeed.Services.Results;

namespace SportsFeed.BackgroundWorkers.EventArgs
{
    public class DatabaseUpdatedEventArgs
    {
        private readonly IDatabaseUpdatedResult changes;

        public DatabaseUpdatedEventArgs()
        {
            this.changes = new DatabaseUpdatedResult();
        }

        public DatabaseUpdatedEventArgs(IDatabaseUpdatedResult changes)
        {
            Guard.WhenArgument(changes, nameof(changes)).IsNull().Throw();

            this.changes = changes;
        }

        public IDatabaseUpdatedResult Changes
        {
            get { return this.changes; }
        }
    }
}
