using SportsFeed.WebClient.Models.Dtos;

namespace SportsFeed.WebClient.Hubs.Clients
{
    public interface IDbUpdatesNotifiedClient
    {
        void SendDeleteData(DbChangeDto dto);

        void SendUpdateData(DbChangeDto dto);
    }
}