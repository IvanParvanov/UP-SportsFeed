using System.Collections.Generic;
using System.Xml.Serialization;

using SportsFeed.Models.Base;
using SportsFeed.Models.Contracts;

namespace SportsFeed.Models
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
        public virtual Sport Sport { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Event;
            if (other == null)
            {
                return false;
            }

            return this.IsLive == other.IsLive
                   && this.SportId == other.SportId
                   && this.CategoryId == other.CategoryId
                   && this.Name == other.Name
                   && this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = (hash * 7) + this.IsLive.GetHashCode();
            hash = (hash * 7) + this.SportId.GetHashCode();
            hash = (hash * 7) + this.CategoryId.GetHashCode();
            hash = (hash * 7) + this.Name.GetHashCode();
            hash = (hash * 7) + this.Id.GetHashCode();
            return hash;
        }
    }
}
