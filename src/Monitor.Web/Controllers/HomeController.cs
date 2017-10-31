using Microsoft.AspNetCore.Mvc;

namespace Monitor.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}