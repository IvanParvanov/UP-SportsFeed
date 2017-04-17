using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using Newtonsoft.Json;

using SportsFeed.Models.Base;
using SportsFeed.Models.Contracts;

namespace SportsFeed.Models
{
    public class Odd : ExternalEntity, IOdd
    {
        [XmlAttribute("Value")]
        public double Value { get; set; }

        [XmlIgnore]
        public int BetId { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Bet Bet { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Odd;
            if (other == null)
            {
                return false;
            }

            return Math.Abs(this.Value - other.Value) < 0.00001
                   && this.BetId == other.BetId
                   && this.Name == other.Name
                   && this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = (hash * 7) + this.Value.GetHashCode();
            hash = (hash * 7) + this.BetId.GetHashCode();
            hash = (hash * 7) + this.Name.GetHashCode();
            hash = (hash * 7) + this.Id.GetHashCode();
            return hash;
        }
    }
}
