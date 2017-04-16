using System.Collections.Generic;
using System.Xml.Serialization;

using SportsFeed.Models.Base;
using SportsFeed.Models.Contracts;

namespace SportsFeed.Models
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
