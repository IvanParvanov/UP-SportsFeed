using Bytes2you.Validation;

using SportsFeed.Services.Results;

namespace SportsFeed.BackgroundWorkers.EventArgs
{
    public class DatabaseCleanedEventArgs
    {
        private readonly IDatabaseCleanedResult changes;

        public DatabaseCleanedEventArgs()
        {
            this.changes = new DatabaseCleanedResult();
        }

        public DatabaseCleanedEventArgs(IDatabaseCleanedResult changes)
        {
            Guard.WhenArgument(changes, nameof(changes)).IsNull().Throw();

            this.changes = changes;
        }

        public IDatabaseCleanedResult Changes
        {
            get
            {
                return this.changes;
            }
        }
    }
}