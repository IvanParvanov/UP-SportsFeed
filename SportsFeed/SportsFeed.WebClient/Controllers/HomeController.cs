using System.Threading.Tasks;
using System.Web.Mvc;

using Bytes2you.Validation;

using SportsFeed.Services.Contracts;

namespace SportsFeed.WebClient.Controllers
{
    public class HomeController : Controller
    {
        public const string DefaultSportName = "Soccer";

        private readonly ISportsService sportsService;

        public HomeController(ISportsService sportsService)
        {
            Guard.WhenArgument(sportsService, nameof(sportsService)).IsNull().Throw();

            this.sportsService = sportsService;
        }

        public async Task<ActionResult> Index(string s = DefaultSportName)
        {
            var sport = await this.sportsService.GetSportByNameAsync(s);
            
            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("_Index", sport);
            }

            return this.View("Index", sport);
        }

        public PartialViewResult Navbar()
        {
            var sports = this.sportsService.GetSportsNames();

            return this.PartialView("_Navbar", sports);
        }
    }
}