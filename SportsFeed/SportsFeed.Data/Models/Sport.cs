using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using SportsFeed.Data.Models.Base;

namespace SportsFeed.Data.Models
{
    public class Sport : ExternalEntity
    {
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        //public virtual ICollection<Event> Events { get; set; }
    }
}
