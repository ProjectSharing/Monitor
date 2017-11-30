using JQCore.Result;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：IWarningSqlApplication.cs
    /// 接口属性：公共
    /// 类功能描述：SQL预警信息业务逻辑接口
    /// 创建标识：template 2017-11-29 22:17:29
    /// </summary>
    public interface IWarningSqlApplication
    {
        /// <summary>
        /// 获取预警日志列表
        /// </summary>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        Task<OperateResult<IPageResult<WarningSqlListDto>>> GetWarningSqlListAsync(WarningSqlPageQueryWhere queryWhere);

        /// <summary>
        /// 获取处理信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperateResult<WarningDealModel>> GetWarningDealModelAsync(int id);

        /// <summary>
        /// 处理警告日志日志
        /// </summary>
        /// <param name="model">处理信息</param>
        /// <returns></returns>
        Task<OperateResult> DealAsync(WarningDealModel model);
    }
}