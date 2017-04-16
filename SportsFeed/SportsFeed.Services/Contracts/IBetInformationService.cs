using System.Collections.Generic;

using SportsFeed.Models;

namespace SportsFeed.Services.Contracts
{
    public interface IBetInformationService
    {
        IList<Sport> GetData();
    }
}
