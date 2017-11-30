using JQCore.Mvc.ActionResult;
using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Web.Controllers
{
    public class WarnLogController : BaseController
    {
        private readonly IWarningLogApplication _warningLogApplication;

        public WarnLogController(IWarningLogApplication warningLogApplication)
        {
            _warningLogApplication = warningLogApplication;
        }

        /// <summary>
        /// 加载警告日志列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(WarningLogPageQueryWhere queryWhere)
        {
            var operateResult = await _warningLogApplication.GetWarningLogListAsync(queryWhere);
            ViewBag.QueryWhere = queryWhere;
            return View(operateResult.Value);
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="id">警告日志记录ID</param>
        /// <returns></returns>
        public Task<IActionResult> Deal(int id)
        {
            return this.ViewResultAsync(() =>
            {
                return _warningLogApplication.GetDealModelAsync(id);
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
                return _warningLogApplication.DealAsync(model);
            });
        }
    }
}