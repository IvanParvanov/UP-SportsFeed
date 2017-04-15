namespace SportsFeed.Data.Models.Contracts
{
    public interface IExternalEntity: IIdentifiable
    {
        int ExternalId { get; set; }
    }
}