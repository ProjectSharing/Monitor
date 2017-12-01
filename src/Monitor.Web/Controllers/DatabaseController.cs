using JQCore.Mvc.ActionResult;
using Microsoft.AspNetCore.Mvc;
using Monitor.Application;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Web.Controllers
{
    public class DatabaseController : BaseController
    {
        private readonly IDatabaseApplication _databaseApplication;

        public DatabaseController(IDatabaseApplication databaseApplication)
        {
            _databaseApplication = databaseApplication;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(DatabasePageQueryWhere queryWhere)
        {
            var operateResult = await _databaseApplication.GetDbListAsync(queryWhere);
            ViewBag.QueryWhere = queryWhere;
            return View(operateResult.Value);
        }

        /// <summary>
        /// 添加数据库信息
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加数据库信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<IActionResult> Add(DatabaseModel model)
        {
            return this.JsonResultAsync(() =>
            {
                model.OperateUserID = AdminID;
                return _databaseApplication.AddDatabaseAsync(model);
            });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">数据库ID</param>
        /// <returns></returns>
        public Task<IActionResult> Edit(int id)
        {
            return this.ViewResultAsync(() =>
            {
                return _databaseApplication.GetDatabaseModelAsync(id);
            });
        }

        /// <summary>
        /// 修改数据库信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<IActionResult> Edit(DatabaseModel model)
        {
            return this.JsonResultAsync(() =>
            {
                model.OperateUserID = AdminID;
                return _databaseApplication.EditDatabaseAsync(model);
            });
        }

        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DatabaseSelectList()
        {
            var operateResult = await _databaseApplication.LoadDbListAsync();
            return operateResult.ToJsonResult();
        }
    }
}