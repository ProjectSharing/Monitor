using JQCore.Result;
using Monitor.DomainService;
using Monitor.Repository;
using Monitor.Trans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：RuntimeSqlApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：执行SQL信息业务逻辑
    /// 创建标识：template 2017-11-29 22:17:30
    /// </summary>
    public sealed class RuntimeSqlApplication : IRuntimeSqlApplication
    {
        private readonly IRuntimeSqlRepository _runtimeSqlRepository;
        private readonly IRuntimeSqlDomainService _runtimeSqlDomainService;

        public RuntimeSqlApplication(IRuntimeSqlRepository runtimeSqlRepository, IRuntimeSqlDomainService runtimeSqlDomainService)
        {
            _runtimeSqlRepository = runtimeSqlRepository;
            _runtimeSqlDomainService = runtimeSqlDomainService;
        }

        /// <summary>
        /// 添加运行sql信息
        /// </summary>
        /// <param name="runtimeSqlModel"></param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> AddRuntimeSqlAsync(RuntimeSqlModel runtimeSqlModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var runtimeSqlInfo = await _runtimeSqlDomainService.AddRuntimeSqlAsync(runtimeSqlModel);
                await _runtimeSqlDomainService.AnalysisRuntimeSqlAsync(runtimeSqlInfo);
            }, callMemberName: "RuntimeSqlApplication-AddRuntimeSqlAsync");
        }

        /// <summary>
        /// 新增运行sql信息
        /// </summary>
        /// <param name="runtimeLogModelList"></param>
        /// <returns>操作结果</returns>
        public OperateResult AddRuntimeSqlList(List<RuntimeSqlModel> runtimeSqlModelList)
        {
            return OperateUtil.Execute(() =>
            {
                foreach (var item in runtimeSqlModelList)
                {
                    AddRuntimeSqlAsync(item).Wait();
                }
            }, callMemberName: "RuntimeSqlApplication-AddRuntimeSqlList");
        }

        /// <summary>
        /// 加载sql运行列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public Task<OperateResult<IPageResult<RuntimeSqlListDto>>> GetRuntimSqlListAsync(RuntimeSqlPageQueryWhere queryWhere)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _runtimeSqlRepository.GetRuntimSqlListAsync(queryWhere);
            });
        }
    }
}