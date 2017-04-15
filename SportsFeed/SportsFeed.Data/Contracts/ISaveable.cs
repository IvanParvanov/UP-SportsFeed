using System.Threading;
using System.Threading.Tasks;

namespace SportsFeed.Data.Contracts
{
    public interface ISaveable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
