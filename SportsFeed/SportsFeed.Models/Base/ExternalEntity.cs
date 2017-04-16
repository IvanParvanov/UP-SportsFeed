using System.Xml.Serialization;

using Microsoft.Build.Framework;

using SportsFeed.Models.Contracts;

namespace SportsFeed.Models.Base
{
    public abstract class ExternalEntity : IExternalEntity
    {
        public int Id { get; set; }

        [XmlAttribute("ID")]
        public int ExternalId { get; set; }

        [Required]
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}
