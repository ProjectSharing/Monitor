using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Domain;
using Monitor.Constant;
using Monitor.Trans;
using JQCore.DataAccess.Utils;
using static Monitor.Constant.SqlConstant;
using static Monitor.Constant.TableConstant;
using JQCore.Result;
using System.Threading.Tasks;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：SysConfigRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：系统配置信息数据访问类
    /// 创建标识：template 2017-10-10 13:32:46
    /// </summary>
    public sealed class SysConfigRepository : BaseDataRepository<SysConfigInfo>, ISysConfigRepository
    {
        public SysConfigRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TABLE_NAME_SYSCONFIG, DbConnConstant.Conn_Name_Monitor)
        {
        }

        /// <summary>
        /// 查找配置列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>配置列表</returns>
        public Task<IPageResult<SysConfigDto>> GetConfigListAsync(SysConfigPageQueryWhere queryWhere)
        {
            SqlWhereBuilder whereBuilder = new SqlWhereBuilder("FIsDeleted=0", DataType);
            whereBuilder.AppendLike("FKey", queryWhere.ConfigKey, paramKey: nameof(queryWhere.ConfigKey));

            const string selectTable = TABLE_NAME_SYSCONFIG + SQLSERVER_WITHNOLOCK;
            const string selectColumn = "FID,FKey,FValue,FComment,ISNULL(FLastModifyTime,FCreateTime) AS FLastModifyTime";
            const string order = "ISNULL(FLastModifyTime,FCreateTime) DESC";
            return QueryPageListAsync<SysConfigDto>(selectColumn, selectTable, whereBuilder.ToString(), order, queryWhere.PageIndex, queryWhere.PageSize, cmdParms: queryWhere);
        }

    }
}
