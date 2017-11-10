using JQCore.Mvc.ActionResult;
using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Threading.Tasks;

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
            return View(new ServicerModel());
        }

        /// <summary>
        /// 添加服务器
        /// </summary>
        /// <param name="model">服务器信息</param>
        /// <returns>操作结果</returns>
        [HttpPost]
        public Task<IActionResult> Add(ServicerModel model)
        {
            return this.JsonResultAsync(() =>
           {
               model.OperateUserID = AdminID;
               return _servicerApplication.AddServicerAsync(model);
           });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">服务器ID</param>
        /// <returns></returns>
        public Task<IActionResult> Edit(int id)
        {
            return this.ViewResultAsync(() =>
          {
              return _servicerApplication.GetServicerModelAsync(id);
          });
        }

        /// <summary>
        /// 修改服务器信息
        /// </summary>
        /// <param name="model">服务器信息</param>
        /// <returns>操作结果</returns>
        [HttpPost]
        public Task<IActionResult> Edit(ServicerModel model)
        {
            return this.JsonResultAsync(() =>
          {
              model.OperateUserID = AdminID;
              return _servicerApplication.EditServicerAsync(model);
          });
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ServicerSelectList()
        {
            var operateResult = await _servicerApplication.LoadServicerListAsync();
            return operateResult.ToJsonResult();
        }
    }
}