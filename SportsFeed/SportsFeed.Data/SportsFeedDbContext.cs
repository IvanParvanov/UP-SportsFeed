using System.Data.Entity;

using SportsFeed.Data.Contracts;

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
}