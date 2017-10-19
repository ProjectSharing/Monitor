using JQCore.Mvc.ActionResult;
using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Web.Controllers
{
    public class SysConfigController : BaseController
    {
        private readonly ISysConfigApplication _sysConfigApplication;

        public SysConfigController(ISysConfigApplication sysConfigApplication)
        {
            _sysConfigApplication = sysConfigApplication;
        }

        /// <summary>
        /// 配置列表页面
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(SysConfigPageQueryWhere queryWhere)
        {
            var operateResult = await _sysConfigApplication.GetConfigListAsync(queryWhere);
            ViewBag.QueryWhere = queryWhere;
            return View(operateResult.Value);
        }

        /// <summary>
        /// 添加配置
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="model">配置信息</param>
        /// <returns>操作结果</returns>
        [HttpPost]
        public Task<IActionResult> Add(SysConfigModel model)
        {
            return this.JsonResultAsync(() =>
            {
                model.OperateUserID = AdminID;
                return _sysConfigApplication.AddConfigAsync(model);
            });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <returns></returns>
        public Task<IActionResult> Edit(int id)
        {
            return this.ViewResultAsync(() =>
            {
                return _sysConfigApplication.GetSysConfigModelAsync(id);
            });
        }

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="model">配置信息</param>
        /// <returns>操作结果</returns>
        [HttpPost]
        public Task<IActionResult> Edit(SysConfigModel model)
        {
            return this.JsonResultAsync(() =>
            {
                model.OperateUserID = AdminID;
                return _sysConfigApplication.EditConfigAsync(model);
            });
        }
    }
}