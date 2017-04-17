using System.Net;

using SportsFeed.Services.Contracts;

namespace SportsFeed.Services
{
    public class WebRequestService : IWebRequestService
    {
        public string DownloadString(string url)
        {
            var client = new WebClient();
            var text = client.DownloadString(url);

            return text;
        }
    }
}
