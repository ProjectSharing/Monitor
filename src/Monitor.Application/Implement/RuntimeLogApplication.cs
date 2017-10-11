using JQCore.Extensions;
using JQCore.Result;
using Monitor.DomainService;
using Monitor.Repository;
using Monitor.Trans;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Monitor.Constant.IgnoreConstant;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：RuntimeLogApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：运行日志信息业务逻辑
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class RuntimeLogApplication : IRuntimeLogApplication
    {
        private readonly IRuntimeLogRepository _runtimeLogRepository;
        private readonly IRuntimeLogDomainService _runtimeLogDomainService;

        public RuntimeLogApplication(IRuntimeLogRepository runtimeLogRepository, IRuntimeLogDomainService runtimeLogDomainService)
        {
            _runtimeLogRepository = runtimeLogRepository;
            _runtimeLogDomainService = runtimeLogDomainService;
        }

        /// <summary>
        /// 新增运行日志
        /// </summary>
        /// <param name="runtimeLogModel">运行日志信息</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> AddLogAsync(RuntimeLogModel runtimeLogModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var runtimeLogInfo = await _runtimeLogDomainService.CreateAsync(runtimeLogModel);
                var id = (await _runtimeLogRepository.InsertOneAsync(runtimeLogInfo, keyName: "FID", ignoreFields: FID)).ToSafeInt32(0);
                runtimeLogInfo.FID = id;
                await _runtimeLogDomainService.AnalysisLogAsync(runtimeLogInfo);
            }, callMemberName: "RuntimeLogApplication-AddLogAsync");
        }

        /// <summary>
        /// 新增运行日志
        /// </summary>
        /// <param name="runtimeLogModelList">运行日志列表</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> AddLogListAsync(List<RuntimeLogModel> runtimeLogModelList)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                await runtimeLogModelList.ForEachAsync(async (item) =>
                 {
                     await AddLogAsync(item);
                 });
            }, callMemberName: "RuntimeLogApplication-AddLogListAsync");
        }
    }
}