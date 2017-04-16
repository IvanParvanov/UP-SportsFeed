using System.Collections.Generic;

namespace SportsFeed.Models.Contracts
{
    public interface IEvent : IExternalEntity
    {
        bool IsLive { get; set; }

        int CategoryId { get; set; }

        HashSet<Match> Matches { get; set; }

        int SportId { get; set; }

        Sport Sport { get; set; }
    }
}
