using System;
using System.Collections.Generic;

using SportsFeed.Models.Enums;

namespace SportsFeed.Models.Contracts
{
    public interface IMatch : IExternalEntity
    {
        DateTime StartDate { get; set; }

        MatchType Type { get; set; }

        HashSet<Bet> Bets { get; set; }

        int EventId { get; set; }

        Event Event { get; set; }
    }
}
