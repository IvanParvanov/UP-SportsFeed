using System.ComponentModel.DataAnnotations.Schema;

using SportsFeed.Data.Models.Contracts;

namespace SportsFeed.Data.Models.Base
{
    public abstract class ExternalEntity : IExternalEntity
    {
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public int ExternalId { get; set; }
    }
}
