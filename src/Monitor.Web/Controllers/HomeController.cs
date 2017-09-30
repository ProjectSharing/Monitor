﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitor.Web.Infrastructure;

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