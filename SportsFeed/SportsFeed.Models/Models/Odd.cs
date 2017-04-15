using System.Xml.Serialization;

using SportsFeed.Models.Models.Base;
using SportsFeed.Models.Models.Contracts;

namespace SportsFeed.Models.Models
{
    public class Odd : ExternalEntity, IOdd
    {
        [XmlAttribute("Value")]
        public double Value { get; set; }

        [XmlIgnore]
        public int BetId { get; set; }

        [XmlIgnore]
        public Bet Bet { get; set; }
    }
}
