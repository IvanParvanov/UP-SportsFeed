using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SportsFeed.Data.Contracts
{
    public interface ISportsFeedDbContext : IDisposable, ISaveable
    {
        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}