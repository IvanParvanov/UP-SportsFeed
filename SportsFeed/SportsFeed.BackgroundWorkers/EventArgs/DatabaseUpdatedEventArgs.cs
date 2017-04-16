using System.Collections.Generic;

using Bytes2you.Validation;

using SportsFeed.Models.Contracts;

namespace SportsFeed.BackgroundWorkers.EventArgs
{
    public class DatabaseUpdatedEventArgs
    {
        private readonly IEnumerable<IExternalEntity> changes;

        public DatabaseUpdatedEventArgs()
        {
            this.changes = new List<IExternalEntity>();
        }

        public DatabaseUpdatedEventArgs(IEnumerable<IExternalEntity> changes)
        {
            Guard.WhenArgument(changes, nameof(changes)).IsNull().Throw();

            this.changes = changes;
        }

        public IEnumerable<IExternalEntity> Changes
        {
            get { return this.changes; }
        }
    }
}
