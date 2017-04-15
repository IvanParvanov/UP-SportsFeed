using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SportsFeed.Models.Models.Base;
using SportsFeed.Models.Models.Contracts;
using SportsFeed.Models.Models.Enums;

namespace SportsFeed.Models.Models
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
        public Event Event { get; set; }
    }
}