using JQCore.Mvc.ActionResult;
using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Web.Controllers
{
    public class WarnSqlController : BaseController
    {
        private readonly IWarningSqlApplication _warningSqlApplication;

        public WarnSqlController(IWarningSqlApplication warningSqlApplication)
        {
            _warningSqlApplication = warningSqlApplication;
        }

        /// <summary>
        /// 加载预警SQL列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(WarningSqlPageQueryWhere queryWhere)
        {
            var operateResult = await _warningSqlApplication.GetWarningSqlListAsync(queryWhere);
            ViewBag.QueryWhere = queryWhere;
            return View(operateResult.Value);
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <returns></returns>
        public Task<IActionResult> Deal(int id)
        {
            return this.ViewResultAsync(() =>
            {
                return _warningSqlApplication.GetWarningDealModelAsync(id);
            });
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<IActionResult> Deal(WarningDealModel model)
        {
            return this.JsonResultAsync(() =>
            {
                model.OperateUserID = AdminID;
                return _warningSqlApplication.DealAsync(model);
            });
        }
    }
}