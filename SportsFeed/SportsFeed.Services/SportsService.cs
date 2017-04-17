using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Bytes2you.Validation;

using SportsFeed.Data.Wrappers;
using SportsFeed.Models;
using SportsFeed.Services.Contracts;

namespace SportsFeed.Services
{
    public class SportsService : ISportsService
    {
        private readonly IEfWrapper<Sport> sportsRepository;

        public SportsService(IEfWrapper<Sport> sportsRepository)
        {
            Guard.WhenArgument(sportsRepository, nameof(sportsRepository)).IsNull().Throw();

            this.sportsRepository = sportsRepository;
        }

        public IEnumerable<string> GetSportsNames()
        {
            var names = this.sportsRepository.All.Select(s => s.Name).Distinct().ToList();

            return names;
        }

        public Task<Sport> GetSportByNameAsync(string sportName)
        {
            var sport = this.sportsRepository.All
                            .Include(s => s.Events)
                            .Include(s => s.Events.Select(e => e.Matches.Select(m => m.Bets.Select(b => b.Odds))))
                            .FirstOrDefaultAsync(s => s.Name == sportName);

            return sport;
        }
    }
}
