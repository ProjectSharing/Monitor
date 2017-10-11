using JQCore.Utils;
using Monitor.Constant;
using Monitor.Domain;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：WarningLogDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：日志预警信息业务逻辑处理
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class WarningLogDomainService : IWarningLogDomainService
    {
        /// <summary>
        /// 创建警告提示信息
        /// </summary>
        /// <param name="runtimeLogInfo">运行日志信息</param>
        /// <returns>警告提示信息</returns>
        public WarningLogInfo Create(RuntimeLogInfo runtimeLogInfo)
        {
            return new WarningLogInfo
            {
                FCreateTime = DateTimeUtil.Now,
                FCreateUserID = -1,
                FDealState = DealState.WaitDeal,
                FIsDeleted = false,
                FNoticeState = NoticeState.WaitNotice,
                FRunTimeLogID = runtimeLogInfo.FID,
                FLogSign = runtimeLogInfo.GetSafeHashID()
            };
        }
    }
}