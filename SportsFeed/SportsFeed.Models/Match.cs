using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SportsFeed.Models.Base;
using SportsFeed.Models.Contracts;
using SportsFeed.Models.Enums;

namespace SportsFeed.Models
{
    public class Match : ExternalEntity, IMatch
    {
        public Match()
        {
            this.Bets = new HashSet<Bet>();
        }

        [XmlAttribute("StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute("MatchType")]
        public MatchType Type { get; set; }

        [XmlElement("Bet")]
        public virtual HashSet<Bet> Bets { get; set; }

        [XmlIgnore]
        public int EventId { get; set; }

        [XmlIgnore]
        public virtual Event Event { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Match;
            if (other == null)
            {
                return false;
            }

            return this.Type == other.Type
                   && this.StartDate == other.StartDate
                   && this.EventId == other.EventId
                   && this.Name == other.Name
                   && this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = (hash * 7) + this.Type.GetHashCode();
            hash = (hash * 7) + this.StartDate.GetHashCode();
            hash = (hash * 7) + this.EventId.GetHashCode();
            hash = (hash * 7) + this.Name.GetHashCode();
            hash = (hash * 7) + this.Id.GetHashCode();
            return hash;
        }
    }
}
