using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitor.Web.Infrastructure;
using Monitor.Application;
using Monitor.Trans;

namespace Monitor.Web.Controllers
{
    public class ServicerController : BaseController
    {
        private readonly IServicerApplication _servicerApplication;
        public ServicerController(IServicerApplication servicerApplication)
        {
            _servicerApplication = servicerApplication;
        }

        /// <summary>
        /// 服务器列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(ServicePageQueryWhere queryWhere)
        {
            var operateResult = await _servicerApplication.GetServiceListAsync(queryWhere);
            ViewBag.QueryWhere = queryWhere;
            return View(operateResult.Value);
        }

        /// <summary>
        /// 添加服务器
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }
    }
}