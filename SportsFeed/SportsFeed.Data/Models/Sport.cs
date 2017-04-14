using System.Collections.Generic;

using SportsFeed.Data.Models.Base;

namespace SportsFeed.Data.Models
{
    public class Sport : VitalBetEntity
    {
        public string Name { get; set; }

        //public virtual ICollection<Event> Events { get; set; }
    }
}
