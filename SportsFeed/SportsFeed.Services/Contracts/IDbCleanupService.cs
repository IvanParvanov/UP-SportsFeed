using SportsFeed.Services.Results;

namespace SportsFeed.Services.Contracts
{
    public interface IDbCleanupService
    {
        IDatabaseCleanedResult CleanStaleData();
    }
}