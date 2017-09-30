using JQCore.Result;
using Monitor.Constant;
using Monitor.DomainService;
using Monitor.Repository;
using Monitor.Trans;
using System.Threading.Tasks;
using static Monitor.Constant.IgnoreConstant;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：ServicerApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器信息业务逻辑
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class ServicerApplication : IServicerApplication
    {
        private readonly IServicerRepository _servcerRepository;
        private readonly IServicerDomainService _servcerDomainService;
        private readonly IOperateLogDomainService _operateLogDomainService;

        public ServicerApplication(IServicerRepository servcerRepository, IServicerDomainService servcerDomainService, IOperateLogDomainService operateLogDomainService)
        {
            _servcerRepository = servcerRepository;
            _servcerDomainService = servcerDomainService;
            _operateLogDomainService = operateLogDomainService;
        }

        /// <summary>
        /// 分页获取服务器列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>服务器列表</returns>
        public Task<OperateResult<IPageResult<ServicerListDto>>> GetServiceListAsync(ServicePageQueryWhere queryWhere)
        {
            return OperateUtil.ExecuteAsync(() =>
            {
                return _servcerRepository.GetServiceListAsync(queryWhere);
            }, callMemberName: "ServicerApplication-GetServiceListAsync");
        }

        /// <summary>
        /// 添加服务器信息
        /// </summary>
        /// <param name="servicerModel">服务器信息</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> AddServicerAsync(ServicerModel servicerModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var servicerInfo = _servcerDomainService.Create(servicerModel);
                await _servcerDomainService.CheckAsync(servicerInfo);
                await _servcerRepository.InsertOneAsync(servicerInfo, keyName: "FID", ignoreFields: FID);
                _operateLogDomainService.AddOperateLog(servicerModel.OperateUserID, OperateModule.Servicer, OperateModuleNode.Add, $"添加:{servicerInfo.GetOperateDesc()}");
            }, callMemberName: "ServicerApplication-AddServicerAsync");
        }

        /// <summary>
        /// 获取服务器的编辑信息
        /// </summary>
        /// <param name="servicerID">服务器ID</param>
        /// <returns>服务器的编辑信息</returns>
        public Task<OperateResult<ServicerModel>> GetServicerModelAsync(int servicerID)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _servcerRepository.GetDtoAsync<ServicerModel>(m => m.FID == servicerID && m.FIsDeleted == false, ignoreFields: new string[] { "OperateUserID" });
            }, callMemberName: "ServicerApplication-GetServicerModelAsync");
        }

        /// <summary>
        /// 修改服务器信息
        /// </summary>
        /// <param name="servicerModel">服务器信息</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> EditServicerAsync(ServicerModel servicerModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var servicerInfo = _servcerDomainService.Create(servicerModel);
                await _servcerDomainService.CheckAsync(servicerInfo);
                await _servcerRepository.UpdateAsync(servicerInfo, m => m.FID == servicerInfo.FID, ignoreFields: IDAndCreate);
                _operateLogDomainService.AddOperateLog(servicerModel.OperateUserID, OperateModule.Servicer, OperateModuleNode.Edit, $"修改:{servicerInfo.GetOperateDesc()}");
            }, callMemberName: "ServicerApplication-AddServicerAsync");
        }
    }
}