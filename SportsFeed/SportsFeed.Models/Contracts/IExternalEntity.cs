namespace SportsFeed.Models.Contracts
{
    public interface IExternalEntity : IIdentifiable
    {
        //int Id { get; set; }

        string Name { get; set; }
    }
}
