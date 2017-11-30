using JQCore.Result;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：IWarningLogApplication.cs
    /// 接口属性：公共
    /// 类功能描述：日志预警信息业务逻辑接口
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public interface IWarningLogApplication
    {
        /// <summary>
        /// 异步查找警告日志列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>警告日志列表</returns>
        Task<OperateResult<IPageResult<WarningLogListDto>>> GetWarningLogListAsync(WarningLogPageQueryWhere queryWhere);

        /// <summary>
        /// 获取处理对象
        /// </summary>
        /// <param name="id">警告日志记录ID</param>
        /// <returns></returns>
        Task<OperateResult<WarningDealModel>> GetDealModelAsync(int id);

        /// <summary>
        /// 处理警告日志日志
        /// </summary>
        /// <param name="model">处理信息</param>
        /// <returns></returns>
        Task<OperateResult> DealAsync(WarningDealModel model);
    }
}