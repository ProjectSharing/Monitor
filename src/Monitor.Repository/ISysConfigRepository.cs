using JQCore.DataAccess.Repositories;
using JQCore.Result;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：ISysConfigRepository.cs
    /// 接口属性：公共
    /// 类功能描述：系统配置信息数据访问接口
    /// 创建标识：template 2017-10-10 13:32:46
    /// </summary>
    public interface ISysConfigRepository : IBaseDataRepository<SysConfigInfo>
    {
        /// <summary>
        /// 查找配置列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>配置列表</returns>
        Task<IPageResult<SysConfigDto>> GetConfigListAsync(SysConfigPageQueryWhere queryWhere);
    }
}