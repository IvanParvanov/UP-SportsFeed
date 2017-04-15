using System.Collections.Generic;

namespace SportsFeed.Models.Models.Contracts
{
    public interface IBet : IExternalEntity
    {
        bool IsLive { get; set; }

        HashSet<Odd> Odds { get; set; }

        int MatchId { get; set; }

        Match Match { get; set; }
    }
}
