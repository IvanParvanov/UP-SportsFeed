using System.Web.Mvc;

namespace SportsFeed.WebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}