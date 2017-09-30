using JQCore;
using JQCore.Utils;
using Monitor.Domain;
using Monitor.Repository;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：ServcerDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器信息业务逻辑处理
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class ServicerDomainService : IServicerDomainService
    {
        private readonly IServicerRepository _servicerRepository;
        public ServicerDomainService(IServicerRepository servicerRepository)
        {
            _servicerRepository = servicerRepository;
        }

        /// <summary>
        /// 根据Model创建服务器信息
        /// </summary>
        /// <param name="servicerModel">Model信息</param>
        /// <returns>服务器信息</returns>
        public ServicerInfo Create(ServicerModel servicerModel)
        {
            servicerModel.NotNull("服务器信息不能为空");
            var servicerInfo = new ServicerInfo
            {
                FComment = servicerModel.FComment,
                FIP = servicerModel.FIP,
                FIsDeleted = false,
                FName = servicerModel.FName,
                FID = servicerModel.FID,
                FMacAddress = servicerModel.FMacAddress,
                FCreateTime = DateTimeUtil.Now,
                FCreateUserID = servicerModel.OperateUserID
            };
            if (servicerModel.FID > 0)
            {
                servicerInfo.FLastModifyTime = DateTimeUtil.Now;
                servicerInfo.FLastModifyUserID = servicerModel.OperateUserID;
            }
            return servicerInfo;
        }

        /// <summary>
        /// 校验服务器信息
        /// </summary>
        /// <param name="servicerInfo">服务器信息</param>
        /// <returns></returns>
        public async Task CheckAsync(ServicerInfo servicerInfo)
        {
            servicerInfo.NotNull("服务器信息不能为空");
            //判断是否有存在相同的Mac地址
            var existMacInfo = await _servicerRepository.GetInfoAsync(m => m.FMacAddress == servicerInfo.FMacAddress && m.FIsDeleted == false);
            if (existMacInfo != null)
            {
                if (existMacInfo.FID != servicerInfo.FID)
                {
                    throw new BizException("Mac地址已存在");
                }
            }
            var existsNameInfo = await _servicerRepository.GetInfoAsync(m => m.FName == servicerInfo.FName && m.FIsDeleted == false);
            if (existsNameInfo != null)
            {
                if (existsNameInfo.FID != servicerInfo.FID)
                {
                    throw new BizException("名字已存在");
                }
            }
        }
    }
}