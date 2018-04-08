using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Park.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ParkControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}