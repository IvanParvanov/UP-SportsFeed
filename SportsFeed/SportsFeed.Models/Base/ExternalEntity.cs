using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

using SportsFeed.Models.Contracts;

namespace SportsFeed.Models.Base
{
    public abstract class ExternalEntity : IExternalEntity
    {
        //[Key]
        //public int Id { get; set; }

        [XmlAttribute("ID")]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}
