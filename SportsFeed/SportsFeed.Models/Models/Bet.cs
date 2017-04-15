using System.Collections.Generic;
using System.Xml.Serialization;

using SportsFeed.Models.Models.Base;
using SportsFeed.Models.Models.Contracts;

namespace SportsFeed.Models.Models
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
        public virtual HashSet<Odd> Odds { get; set; }

        [XmlIgnore]
        public int MatchId { get; set; }

        [XmlIgnore]
        public Match Match { get; set; }
    }
}