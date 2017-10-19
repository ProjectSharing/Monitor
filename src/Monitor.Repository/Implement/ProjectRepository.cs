using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using JQCore.DataAccess.Utils;
using JQCore.Result;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;
using static Monitor.Constant.SqlConstant;
using static Monitor.Constant.TableConstant;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：ProjectRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：项目信息数据访问类
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class ProjectRepository : BaseDataRepository<ProjectInfo>, IProjectRepository
    {
        public ProjectRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TABLE_NAME_PROJECT, DbConnConstant.Conn_Name_Monitor)
        {
        }

        /// <summary>
        /// 分页获取项目列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>项目列表</returns>
        public Task<IPageResult<ProjectListDto>> GetProjectListAsync(ProjectPageQueryWhere queryWhere)
        {
            SqlWhereBuilder whereBuilder = new SqlWhereBuilder("FIsDeleted=0", DataType);
            whereBuilder.AppendLike("FName", queryWhere.Name, paramKey: nameof(queryWhere.Name));

            const string selectTable = TABLE_NAME_PROJECT + SQLSERVER_WITHNOLOCK;
            const string selectColumn = "FID,FName,FComment,ISNULL(FLastModifyTime,FCreateTime) AS FLastModifyTime";
            const string order = "ISNULL(FLastModifyTime,FCreateTime) DESC";
            return QueryPageListAsync<ProjectListDto>(selectColumn, selectTable, whereBuilder.ToString(), order, queryWhere.PageIndex, queryWhere.PageSize, cmdParms: queryWhere);
        }
    }
}