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
    /// 类名：ServicerRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器信息数据访问类
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class ServicerRepository : BaseDataRepository<ServicerInfo>, IServicerRepository
    {
        public ServicerRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_SERVCER, DbConnConstant.Conn_Name_Monitor)
        {
        }

        /// <summary>
        /// 分页获取服务器列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>服务器列表</returns>
        public Task<IPageResult<ServicerListDto>> GetServiceListAsync(ServicePageQueryWhere queryWhere)
        {
            SqlWhereBuilder whereBuilder = new SqlWhereBuilder("FIsDeleted=0", DataType);
            whereBuilder.AppendEqual("FMacAddress", queryWhere.MacAddress, paramKey: nameof(queryWhere.MacAddress))
                        .AppendLike("FIP", queryWhere.IP, paramKey: nameof(queryWhere.IP))
                        .AppendLike("FName", queryWhere.Name, paramKey: nameof(queryWhere.Name))
                        ;
            const string selectTable = TABLE_NAME_SERVCER + SQLSERVER_WITHNOLOCK;
            const string selectColumn = "FID,FMacAddress,FIP,FName,FComment,ISNULL(FLastModifyTime,FCreateTime) AS FLastModifyTime";
            const string order = "ISNULL(FLastModifyTime,FCreateTime) DESC";
            return QueryPageListAsync<ServicerListDto>(selectColumn, selectTable, whereBuilder.ToString(), order, queryWhere.PageIndex, queryWhere.PageSize, cmdParms: queryWhere);
        }
    }
}