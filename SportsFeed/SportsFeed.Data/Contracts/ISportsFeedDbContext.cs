using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using SportsFeed.Models.Models;

namespace SportsFeed.Data.Contracts
{
    public interface ISportsFeedDbContext : IDisposable, ISaveable
    {
        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        IDbSet<Sport> Sports { get; set; }

        IDbSet<Event> Events { get; set; }

        IDbSet<Match> Matches { get; set; }

        IDbSet<Bet> Bets { get; set; }

        IDbSet<Odd> Odds { get; set; }
    }
}
