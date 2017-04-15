using System.Collections.Generic;
using System.Xml.Serialization;

using SportsFeed.Models.Models.Base;
using SportsFeed.Models.Models.Contracts;

namespace SportsFeed.Models.Models
{
    public class Event : ExternalEntity, IEvent
    {
        public Event()
        {
            this.Matches = new HashSet<Match>();
        }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlAttribute("CategoryID")]
        public int CategoryId { get; set; }

        [XmlElement("Match")]
        public virtual HashSet<Match> Matches { get; set; }

        [XmlIgnore]
        public int SportId { get; set; }

        [XmlIgnore]
        public Sport Sport { get; set; }
    }
}