namespace SportsFeed.Models.Contracts
{
    public interface IOdd : IExternalEntity
    {
        double Value { get; set; }

        int BetId { get; set; }

        Bet Bet { get; set; }
    }
}