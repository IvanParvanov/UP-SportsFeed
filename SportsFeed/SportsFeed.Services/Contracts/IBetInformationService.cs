using System.Collections.Generic;

using SportsFeed.Models.Models;
using SportsFeed.Models.Models.Contracts;

namespace SportsFeed.Services.Contracts
{
    public interface IBetInformationService
    {
        IEnumerable<Sport> GetData();
    }
}
