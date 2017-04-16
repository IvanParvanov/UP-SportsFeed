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

        public override bool Equals(object obj)
        {
            var other = obj as Match;
            if (other == null)
            {
                return false;
            }

            return this.Name == other.Name
                   && this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = (hash * 7) + this.Name.GetHashCode();
            hash = (hash * 7) + this.Id.GetHashCode();
            return hash;
        }
    }
}
