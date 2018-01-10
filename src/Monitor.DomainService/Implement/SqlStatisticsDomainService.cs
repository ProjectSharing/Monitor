using Monitor.Cache;
using Monitor.Domain;
using Monitor.Repository;
using System.Threading.Tasks;
using JQCore.Utils;
using Monitor.Constant;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：SqlStatisticsDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SQL统计业务逻辑处理
    /// 创建标识：template 2017-12-02 13:19:31
    /// </summary>
    public sealed class SqlStatisticsDomainService : ISqlStatisticsDomainService
    {
        private readonly IProjectCache _projectCache;
        public SqlStatisticsDomainService(IProjectCache projectCache)
        {
            _projectCache = projectCache;
        }

        /// <summary>
        /// 根据项目来统计
        /// </summary>
        /// <returns></returns>
        private async Task StatisticsByProjectAsync()
        {
            var projectList = await _projectCache.GetProjectListAsync();
            //获取上次各个统计截止时间
            var statisticsValueTypeList = typeof(StatisticsValueType).GetDesc().Keys;
            foreach (var statisticsValueType in statisticsValueTypeList)
            {

            }
        }
    }
}
