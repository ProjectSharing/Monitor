using JQCore.Utils;
using JQCore.Web;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using System.Threading.Tasks;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：WarningInfoOperateLogDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：预警信息操作记录业务逻辑处理
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class WarningInfoOperateLogDomainService : IWarningInfoOperateLogDomainService
    {
        private readonly IWarningInfoOperateLogRepository _warningInfoOperateLogRepository;

        public WarningInfoOperateLogDomainService(IWarningInfoOperateLogRepository warningInfoOperateLogRepository)
        {
            _warningInfoOperateLogRepository = warningInfoOperateLogRepository;
        }

        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="keyID">主键</param>
        /// <param name="content">操作内容</param>
        /// <param name="operateUserID">操作人</param>
        /// <returns></returns>
        public async Task AddLogAsync(int type, int keyID, string content, int operateUserID)
        {
            var warningInfoOperateLogInfo = Create(type, keyID, content, operateUserID);
            await _warningInfoOperateLogRepository.InsertOneAsync(warningInfoOperateLogInfo, ignoreFields: IgnoreConstant.FID);
        }

        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="keyID">主键</param>
        /// <param name="content">操作内容</param>
        /// <param name="operateUserID">操作人</param>
        public void AddLog(int type, int keyID, string content, int operateUserID)
        {
            var warningInfoOperateLogInfo = Create(type, keyID, content, operateUserID);
            _warningInfoOperateLogRepository.InsertOne(warningInfoOperateLogInfo, ignoreFields: IgnoreConstant.FID);
        }

        /// <summary>
        /// 创建操作记录信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="keyID">主键</param>
        /// <param name="content">操作内容</param>
        /// <param name="operateUserID">操作人</param>
        /// <returns>操作记录信息</returns>
        public WarningInfoOperateLogInfo Create(int type, int keyID, string content, int operateUserID)
        {
            return new WarningInfoOperateLogInfo
            {
                FType = type,
                FCreateTime = DateTimeUtil.Now,
                FIsDeleted = false,
                FCreateUserID = operateUserID,
                FOperateContent = content,
                FWarningInfoID = keyID,
                FOperateIP = WebHttpContext.RealIP,
                FOperateUrl = WebHttpContext.AbsoluteUrl
            };
        }
    }
}