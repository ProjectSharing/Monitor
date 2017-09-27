using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitor.Web.Infrastructure;

namespace Monitor.Web.Controllers
{
    public class ServicerController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}