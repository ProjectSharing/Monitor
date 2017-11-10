using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Web.Controllers
{
    public class RuntimeLogController : Controller
    {
        private readonly IRuntimeLogApplication _runtimeLogApplication;

        public RuntimeLogController(IRuntimeLogApplication runtimeLogApplication)
        {
            _runtimeLogApplication = runtimeLogApplication;
        }

        /// <summary>
        /// 加载运行日志列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(RuntimeLogPageQueryWhere queryWhere)
        {
            var operateResult = await _runtimeLogApplication.GetRuntimeLogListAsync(queryWhere);
            ViewBag.QueryWhere = queryWhere;
            return View(operateResult.Value);
        }
    }
}