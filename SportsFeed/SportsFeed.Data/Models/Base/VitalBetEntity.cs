using SportsFeed.Data.Models.Contracts;

namespace SportsFeed.Data.Models.Base
{
    public abstract class VitalBetEntity : IVitalBetEntity
    {
        public int Id { get; set; }

        public int VitalBetId { get; set; }
    }
}
