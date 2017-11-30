using JQCore.Utils;
using Monitor.Domain;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：WarningSqlDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SQL预警信息业务逻辑处理
    /// 创建标识：template 2017-11-29 22:17:29
    /// </summary>
    public sealed class WarningSqlDomainService : IWarningSqlDomainService
    {
        /// <summary>
        /// 创建sql的警告信息
        /// </summary>
        /// <param name="runtimeSqlInfo"></param>
        /// <returns></returns>
        public WarningSqlInfo Create(RuntimeSqlInfo runtimeSqlInfo)
        {
            return new WarningSqlInfo
            {
                FCreateTime = DateTimeUtil.Now,
                FCreateUserID = -1,
                FDealState = Constant.DealState.WaitDeal,
                FIsDeleted = false,
                FNoticeState = Constant.NoticeState.WaitNotice,
                FRuntimeSqlID = runtimeSqlInfo.FID,
                FSqlSign = runtimeSqlInfo.GetSafeHashID()
            };
        }
    }
}