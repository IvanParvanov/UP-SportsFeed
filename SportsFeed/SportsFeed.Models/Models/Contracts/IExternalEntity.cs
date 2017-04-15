namespace SportsFeed.Models.Models.Contracts
{
    public interface IExternalEntity : IIdentifiable
    {
        int ExternalId { get; set; }

        string Name { get; set; }
    }
}
