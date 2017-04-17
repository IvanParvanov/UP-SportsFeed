using System.Collections.Generic;
using System.Threading.Tasks;

using SportsFeed.Models;

namespace SportsFeed.Services.Contracts
{
    public interface ISportsService
    {
        IEnumerable<string> GetSportsNames();

        Task<Sport> GetSportByNameAsync(string sportName);
    }
}