using System.Collections.Generic;
using System.Xml.Serialization;

using SportsFeed.Models.Models.Base;
using SportsFeed.Models.Models.Contracts;

namespace SportsFeed.Models.Models
{
    public class Sport : ExternalEntity, ISport
    {
        public Sport()
        {
            this.Events = new HashSet<Event>();
        }

        [XmlElement("Event")]
        public virtual HashSet<Event> Events { get; set; }
    }
}
