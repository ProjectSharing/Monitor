using JQCore;
using JQCore.Utils;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：SysConfigDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：系统配置信息业务逻辑处理
    /// 创建标识：template 2017-10-10 13:32:46
    /// </summary>
    public sealed class SysConfigDomainService : ISysConfigDomainService
    {
        private readonly ISysConfigRepository _sysConfigRepository;

        public SysConfigDomainService(ISysConfigRepository sysConfigRepository)
        {
            _sysConfigRepository = sysConfigRepository;
        }

        /// <summary>
        /// 创建配置信息
        /// </summary>
        /// <param name="sysConfigModel">配置信息</param>
        /// <returns>配置信息</returns>
        public SysConfigInfo Create(SysConfigModel sysConfigModel)
        {
            sysConfigModel.NotNull("配置信息不能为空");
            sysConfigModel.FKey.NotNullAndNotEmptyWhiteSpace("Key不能为空");
            var sysConfigInfo = new SysConfigInfo
            {
                FComment = sysConfigModel.FComment,
                FCreateTime = DateTimeUtil.Now,
                FCreateUserID = sysConfigModel.OperateUserID.Value,
                FIsDeleted = false,
                FKey = sysConfigModel.FKey.Trim(),
                FValue = sysConfigModel.FValue,
                FID = sysConfigModel.FID ?? 0
            };
            if (sysConfigInfo.FID > 0)
            {
                sysConfigInfo.FLastModifyTime = DateTimeUtil.Now;
                sysConfigInfo.FLastModifyUserID = sysConfigModel.OperateUserID;
            }
            return sysConfigInfo;
        }

        /// <summary>
        /// 校验服务器信息
        /// </summary>
        /// <param name="servicerInfo">服务器信息</param>
        /// <returns></returns>
        public async Task CheckAsync(SysConfigInfo sysConfigInfo)
        {
            sysConfigInfo.NotNull("配置信息不能为空");
            //判断是否有存在相同的Mac地址
            var existsConfigInfo = await _sysConfigRepository.GetInfoAsync(m => m.FKey == sysConfigInfo.FKey && m.FIsDeleted == false);
            if (existsConfigInfo != null)
            {
                if (existsConfigInfo.FID != sysConfigInfo.FID)
                {
                    throw new BizException("Key已存在");
                }
            }
        }

        /// <summary>
        /// 配置更改
        /// </summary>
        /// <param name="operateType">更改类型</param>
        /// <param name="servicerID">配置ID</param>
        /// <returns></returns>
        public Task ConfigChangedAsync(OperateType operateType, int configID)
        {
            //TODO 更新服务器缓存
            return Task.Delay(1);
        }
    }
}