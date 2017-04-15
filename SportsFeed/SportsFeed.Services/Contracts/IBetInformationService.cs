using System.Collections.Generic;

using SportsFeed.Data.Models.Contracts;

namespace SportsFeed.Services.Contracts
{
    public interface IBetInformationService
    {
        IEnumerable<ISport> GetData();
    }
}
