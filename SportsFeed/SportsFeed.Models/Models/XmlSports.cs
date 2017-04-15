using System.Xml.Serialization;

namespace SportsFeed.Models.Models
{
    [XmlRoot("XmlSports")]
    public class XmlSports
    {
        [XmlElement("Sport")]
        public virtual Sport[] Sports { get; set; }
    }
}
