using System.Collections.Generic;

namespace SportsFeed.Data.Results
{
    public interface IDataModifiedResultFactory
    {
        IDataModifiedResult CreateDatabaseUpdateResult(bool isSuccessfull, IEnumerable<string> errors = null);
    }
}
