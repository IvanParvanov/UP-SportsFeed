using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace SportsFeed.Data
{
    public class SportsFeedDbContext : DbContext, ISportsFeedDbContext
    {
        public SportsFeedDbContext()
            : base("name=SportsFeedDbContext")
        {
            this.Database.CreateIfNotExists();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }

    public interface ISportsFeedDbContext : IDisposable, ISaveable
    {
        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }

    public interface ISaveable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}