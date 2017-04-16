namespace SportsFeed.Models.Contracts
{
    public interface IExternalEntity : IIdentifiable
    {
        int ExternalId { get; set; }

        string Name { get; set; }
    }
}
