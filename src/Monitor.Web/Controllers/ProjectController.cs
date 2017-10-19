using JQCore.Mvc.ActionResult;
using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Monitor.Web.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectApplication _projectApplication;

        public ProjectController(IProjectApplication projectApplication)
        {
            _projectApplication = projectApplication;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(ProjectPageQueryWhere queryWhere)
        {
            var operateResult = await _projectApplication.GetProjectListAsync(queryWhere);
            ViewBag.QueryWhere = queryWhere;
            return View(operateResult.Value);
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="model">项目信息</param>
        /// <returns>操作结果</returns>
        [HttpPost]
        public Task<IActionResult> Add(ProjectModel model)
        {
            return this.JsonResultAsync(() =>
            {
                model.OperateUserID = AdminID;
                return _projectApplication.AddProjectAsync(model);
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
                return _projectApplication.GetProjectModelAsync(id);
            });
        }

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="model">配置信息</param>
        /// <returns>操作结果</returns>
        [HttpPost]
        public Task<IActionResult> Edit(ProjectModel model)
        {
            return this.JsonResultAsync(() =>
            {
                model.OperateUserID = AdminID;
                return _projectApplication.EditProjectAsync(model);
            });
        }
    }
}