using System.Collections.Generic;

using SportsFeed.Models;

namespace SportsFeed.WebClient.Models.Dtos
{
    public class DbChangeDto
    {
        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<Match> Matches { get; set; }

        public IEnumerable<Bet> Bets { get; set; }

        public IEnumerable<Odd> Odds { get; set; }
    }
}