using System.Collections.Generic;
using System.Threading.Tasks;

using SportsFeed.Models;

namespace SportsFeed.Services.Contracts
{
    public interface IGroupBySportService
    {
        Task<Dictionary<string, IEnumerable<Event>>> GetEventsBySportAsync(ICollection<int> eventIds);

        Task<Dictionary<string, IEnumerable<Match>>> GetMatchesBySportAsync(ICollection<int> matchIds);

        Task<Dictionary<string, IEnumerable<Bet>>> GetBetsBySportAsync(ICollection<int> betIds);

        Task<Dictionary<string, IEnumerable<Odd>>> GetOddsBySportAsync(ICollection<int> oddIds);
    }
}