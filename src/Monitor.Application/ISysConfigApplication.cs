using JQCore.Result;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：ISysConfigApplication.cs
    /// 接口属性：公共
    /// 类功能描述：系统配置信息业务逻辑接口
    /// 创建标识：template 2017-10-10 13:32:46
    /// </summary>
    public interface ISysConfigApplication
    {
        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>配置列表</returns>
        Task<OperateResult<IPageResult<SysConfigListDto>>> GetConfigListAsync(SysConfigPageQueryWhere queryWhere);

        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="sysConfigModel">配置信息</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> AddConfigAsync(SysConfigModel sysConfigModel);

        /// <summary>
        /// 获取配置编辑信息
        /// </summary>
        /// <param name="configID">配置ID</param>
        /// <returns>配置编辑信息</returns>
        Task<OperateResult<SysConfigModel>> GetSysConfigModelAsync(int configID);

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="sysConfigModel">配置信息</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> EditConfigAsync(SysConfigModel sysConfigModel);

        /// <summary>
        /// 同步配置信息
        /// </summary>
        /// <returns>操作结果</returns>
        Task<OperateResult> SynchroConfigAsync();
    }
}