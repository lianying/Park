using System.Web.Mvc;

namespace Park.Web.Controllers
{
    public class AboutController : ParkControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}