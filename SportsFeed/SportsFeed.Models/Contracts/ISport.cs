using System.Collections.Generic;

namespace SportsFeed.Models.Contracts
{
    public interface ISport : IExternalEntity
    {
        HashSet<Event> Events { get; set; }
    }
}
