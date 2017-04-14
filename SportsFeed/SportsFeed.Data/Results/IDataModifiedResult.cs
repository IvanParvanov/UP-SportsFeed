using System.Collections.Generic;

namespace SportsFeed.Data.Results
{
    public interface IDataModifiedResult
    {
        bool Successful { get; }

        IEnumerable<string> Errors { get; }
    }
}
