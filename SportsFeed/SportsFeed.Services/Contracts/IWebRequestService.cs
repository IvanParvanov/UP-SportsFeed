namespace SportsFeed.Services.Contracts
{
    public interface IWebRequestService
    {
        string DownloadString(string url);
    }
}