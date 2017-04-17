using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using Newtonsoft.Json;

using SportsFeed.Models.Base;
using SportsFeed.Models.Contracts;

namespace SportsFeed.Models
{
    public class Bet : ExternalEntity, IBet
    {
        public Bet()
        {
            this.Odds = new HashSet<Odd>();
        }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlElement("Odd")]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual HashSet<Odd> Odds { get; set; }

        [XmlIgnore]
        public int MatchId { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Match Match { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Bet;
            if (other == null)
            {
                return false;
            }

            return this.IsLive == other.IsLive
                   && this.MatchId == other.MatchId
                   && this.Name == other.Name
                   && this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = (hash * 7) + this.IsLive.GetHashCode();
            hash = (hash * 7) + this.MatchId.GetHashCode();
            hash = (hash * 7) + this.Name.GetHashCode();
            hash = (hash * 7) + this.Id.GetHashCode();
            return hash;
        }
    }
}
