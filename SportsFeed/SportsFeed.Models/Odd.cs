using System.Xml.Serialization;

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
        public virtual Bet Bet { get; set; }
    }
}
