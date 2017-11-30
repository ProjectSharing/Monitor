using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Web.Controllers
{
    public class RuntimeSqlController : Controller
    {
        private readonly IRuntimeSqlApplication _runtimeSqlApplication;

        public RuntimeSqlController(IRuntimeSqlApplication runtimeSqlApplication)
        {
            _runtimeSqlApplication = runtimeSqlApplication;
        }

        /// <summary>
        /// 加载运行SQL列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(RuntimeSqlPageQueryWhere queryWhere)
        {
            var operateResult = await _runtimeSqlApplication.GetRuntimSqlListAsync(queryWhere);
            ViewBag.QueryWhere = queryWhere;
            return View(operateResult.Value);
        }
    }
}