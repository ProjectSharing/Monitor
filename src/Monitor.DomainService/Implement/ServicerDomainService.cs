using JQCore;
using JQCore.Extensions;
using JQCore.Utils;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using Monitor.Trans;
using System;
using System.Threading.Tasks;
using static Monitor.Constant.LockKeyConstant;

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

        /// <summary>
        /// 创建服务器，当名字不存在时
        /// </summary>
        /// <param name="servicerMac">Mac地址</param>
        /// <returns>项目信息</returns>
        public Task<ServicerInfo> AddWhenNotExistAsync(string servicerMac)
        {
            servicerMac.NotNull("Mac地址不能为空");
            var servicerInfo = new ServicerInfo
            {
                FCreateTime = DateTimeUtil.Now,
                FIsDeleted = false,
                FMacAddress = servicerMac,
                FCreateUserID = -1
            };
            return LockUtil.ExecuteWithLockAsync(Lock_ServicerModify, servicerMac, TimeSpan.FromMinutes(2), async () =>
            {
                var existServicerInfo = await _servicerRepository.GetInfoAsync(m => m.FIsDeleted == false && m.FMacAddress == servicerMac);
                if (existServicerInfo != null)
                {
                    return existServicerInfo;
                }
                else
                {
                    int servicerID = (await _servicerRepository.InsertOneAsync(servicerInfo, keyName: "FID", ignoreFields: IgnoreConstant.FID)).ToSafeInt32(0);
                    servicerInfo.FID = servicerID;
                    await ServicerChangedAsync(OperateType.Add, servicerID);
                    return servicerInfo;
                }
            });
        }

        /// <summary>
        /// 服务器更改
        /// </summary>
        /// <param name="operateType">更改类型</param>
        /// <param name="servicerID">服务器ID</param>
        /// <returns></returns>
        public Task ServicerChangedAsync(OperateType operateType, int servicerID)
        {
            //TODO 更新服务器缓存
            return Task.Delay(1);
        }
    }
}