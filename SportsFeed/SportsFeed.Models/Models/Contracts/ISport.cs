using System.Collections.Generic;

namespace SportsFeed.Models.Models.Contracts
{
    public interface ISport : IExternalEntity
    {
        HashSet<Event> Events { get; set; }
    }
}
