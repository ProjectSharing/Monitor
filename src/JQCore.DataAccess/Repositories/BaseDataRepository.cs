using JQCore.DataAccess.DbClient;
using JQCore.DataAccess.Utils;
using JQCore.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JQCore.DataAccess.Repositories
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BaseDataRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：基础数据访问
    /// 创建标识：yjq 2017/9/5 20:45:16
    /// </summary>
    public class BaseDataRepository<T> : IBaseDataRepository<T> where T : class, new()
    {
        private readonly IDataAccessFactory _dataAccessFactory;
        private readonly string _configName;
        private readonly string _tableName;
        private readonly DatabaseType _readDateType;
        private readonly DatabaseType _writerDataType;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="dataAccessFactory"></param>
        /// <param name="tableName"></param>
        /// <param name="configName"></param>
        public BaseDataRepository(IDataAccessFactory dataAccessFactory, string tableName, string configName)
        {
            _dataAccessFactory = dataAccessFactory;
            _tableName = tableName;
            _configName = configName;
            var dataProperty = DBSettings.GetDatabaseProperty(ConfigName);
            if (dataProperty != null)
            {
                _readDateType = dataProperty.Reader.DatabaseType;
                _writerDataType = dataProperty.Writer.DatabaseType;
            }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        protected virtual DatabaseType ReaderDataType
        {
            get
            {
                return _readDateType;
            }
        }

        protected virtual DatabaseType WriterDataType
        {
            get
            {
                return _writerDataType;
            }
        }

        /// <summary>
        /// 表名
        /// </summary>
        protected string TableName { get { return _tableName; } }

        /// <summary>
        /// 配置文件名字
        /// </summary>
        protected string ConfigName { get { return _configName; } }

        #region 获取数据操作

        /// <summary>
        /// 数据库创建工厂
        /// </summary>
        protected IDataAccessFactory DataAccessFactory
        {
            get
            {
                return _dataAccessFactory;
            }
        }

        /// <summary>
        /// 获取数据操作
        /// </summary>
        /// <param name="isWrite">是否需要执行写操作</param>
        /// <returns>数据操作</returns>
        protected IDataAccess GetDataAccess(bool isWrite = true)
        {
            return _dataAccessFactory.GetDataAccess(_configName, isWriter: isWrite);
        }

        #endregion 获取数据操作

        /// <summary>
        /// 插入单挑数据
        /// </summary>
        /// <param name="info">对象值</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        /// <returns>如果是自增则返回自增值，不是返回新增行数</returns>
        public object InsertOne(T info, string keyName = null, string[] ignoreFields = null, bool isIdentity = true)
        {
            SqlQuery query = SqlQueryUtil.BuilderInsertOneSqlQuery(info, TableName, keyName: keyName, ignoreFields: ignoreFields, isIdentity: isIdentity, dbType: WriterDataType);
            if (isIdentity)
            {
                return GetDataAccess().ExecuteScalar<object>(query);
            }
            return GetDataAccess().ExecuteNonQuery(query).ToString();
        }

        /// <summary>
        /// 异步插入单挑数据
        /// </summary>
        /// <param name="info">对象值</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        /// <returns>如果是自增则返回自增值，不是返回新增行数</returns>
        public async Task<object> InsertOneAsync(T info, string keyName = null, string[] ignoreFields = null, bool isIdentity = true)
        {
            SqlQuery query = SqlQueryUtil.BuilderInsertOneSqlQuery(info, TableName, keyName: keyName, ignoreFields: ignoreFields, isIdentity: isIdentity, dbType: WriterDataType);
            if (isIdentity)
            {
                return await GetDataAccess().ExecuteScalarAsync<object>(query);
            }
            return (await GetDataAccess().ExecuteNonQueryAsync(query)).ToString();
        }

        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="infoList">要插入的数据列表</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        public void InsertMany(List<T> infoList, string keyName = null, string[] ignoreFields = null, bool isIdentity = true)
        {
            SqlQuery query = SqlQueryUtil.BuilderInsertManySqlQuery(infoList, TableName, keyName: keyName, ignoreFields: ignoreFields, isIdentity: isIdentity, dbType: WriterDataType);
            GetDataAccess().ExecuteNonQuery(query);
        }

        /// <summary>
        /// 异步插入多条数据
        /// </summary>
        /// <param name="infoList">要插入的数据列表</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        /// <returns></returns>
        public Task InsertManyAsync(List<T> infoList, string keyName = null, string[] ignoreFields = null, bool isIdentity = true)
        {
            SqlQuery query = SqlQueryUtil.BuilderInsertManySqlQuery(infoList, TableName, keyName: keyName, ignoreFields: ignoreFields, isIdentity: isIdentity, dbType: WriterDataType);
            return GetDataAccess().ExecuteNonQueryAsync(query);
        }

        /// <summary>
        /// 批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="infoList">要插入的列表</param>
        public void BulkInsert(List<T> infoList)
        {
            GetDataAccess().BulkInsert(infoList, TableName);
        }

        /// <summary>
        /// 异步批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="infoList">要插入的列表</param>
        /// <returns>Task</returns>
        public Task BulkInsertAsync(List<T> infoList)
        {
            return GetDataAccess().BulkInsertAsync(infoList, TableName);
        }

        /// <summary>
        /// 批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="dataTable">要插入的表格</param>
        public void BulkInsert(DataTable dataTable)
        {
            GetDataAccess().BulkInsert(dataTable, TableName);
        }

        /// <summary>
        /// 异步批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="dataTable">要插入的表格</param>
        /// <returns>Task</returns>
        public Task BulkInsertAsync(DataTable dataTable)
        {
            return GetDataAccess().BulkInsertAsync(dataTable, TableName);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="data">要更新的信息</param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <returns>受影响行数</returns>
        public int Update(object data, object condition, string[] ignoreFields = null)
        {
            SqlQuery query = SqlQueryUtil.BuilderUpdateSqlQuery(data, condition, TableName, ignoreFields: ignoreFields, dbType: WriterDataType);
            return GetDataAccess().ExecuteNonQuery(query);
        }

        /// <summary>
        /// 异步更新信息
        /// </summary>
        /// <param name="data">要更新的信息</param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <returns>受影响行数</returns>
        public Task<int> UpdateAsync(object data, object condition, string[] ignoreFields = null)
        {
            SqlQuery query = SqlQueryUtil.BuilderUpdateSqlQuery(data, condition, TableName, ignoreFields: ignoreFields, dbType: WriterDataType);
            return GetDataAccess().ExecuteNonQueryAsync(query);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="data">要更新的信息</param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <returns>受影响行数</returns>
        public int Update(object data, Expression<Func<T, bool>> condition, string[] ignoreFields = null)
        {
            SqlQuery query = SqlQueryUtil.BuilderUpdateSqlQuery(data, condition, TableName, ignoreFields: ignoreFields, dbType: WriterDataType);
            return GetDataAccess().ExecuteNonQuery(query);
        }

        /// <summary>
        /// 异步更新信息
        /// </summary>
        /// <param name="data">要更新的信息</param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <returns>受影响行数</returns>
        public Task<int> UpdateAsync(object data, Expression<Func<T, bool>> condition, string[] ignoreFields = null)
        {
            SqlQuery query = SqlQueryUtil.BuilderUpdateSqlQuery(data, condition, TableName, ignoreFields: ignoreFields, dbType: WriterDataType);
            return GetDataAccess().ExecuteNonQueryAsync(query);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>受影响的行数</returns>
        public int Delete(object condition)
        {
            SqlQuery query = SqlQueryUtil.BuilderDeleteSqlQuery(condition, TableName, dbType: WriterDataType);
            return GetDataAccess().ExecuteNonQuery(query);
        }

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>受影响的行数</returns>
        public Task<int> DeleteAsync(object condition)
        {
            SqlQuery query = SqlQueryUtil.BuilderDeleteSqlQuery(condition, TableName, dbType: WriterDataType);
            return GetDataAccess().ExecuteNonQueryAsync(query);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>受影响的行数</returns>
        public int Delete(Expression<Func<T, bool>> condition)
        {
            SqlQuery query = SqlQueryUtil.BuilderDeleteSqlQuery(condition, TableName, dbType: WriterDataType);
            return GetDataAccess().ExecuteNonQuery(query);
        }

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>受影响的行数</returns>
        public Task<int> DeleteAsync(Expression<Func<T, bool>> condition)
        {
            SqlQuery query = SqlQueryUtil.BuilderDeleteSqlQuery(condition, TableName, dbType: WriterDataType);
            return GetDataAccess().ExecuteNonQueryAsync(query);
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="condition">条件</param>
        /// <param name="isWrite">是否为写连接</param>
        /// <returns>最小值</returns>
        public TReturn Min<TProperty, TReturn>(Expression<Func<T, TProperty>> expression, Expression<Func<T, bool>> condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryMinSqlQuery(expression, TableName, condition, dbType: WriterDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefault<TReturn>(query);
        }

        /// <summary>
        /// 异步获取最小值
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="condition">条件</param>
        /// <param name="isWrite">是否为写连接</param>
        /// <returns>最小值</returns>
        public Task<TReturn> MinAsync<TProperty, TReturn>(Expression<Func<T, TProperty>> expression, Expression<Func<T, bool>> condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryMinSqlQuery(expression, TableName, condition, dbType: WriterDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefaultAsync<TReturn>(query);
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="condition">条件</param>
        /// <param name="isWrite">是否为写连接</param>
        /// <returns>最大值</returns>
        public TReturn Max<TProperty, TReturn>(Expression<Func<T, TProperty>> expression, Expression<Func<T, bool>> condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryMaxSqlQuery(expression, TableName, condition, dbType: WriterDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefault<TReturn>(query);
        }

        /// <summary>
        /// 异步获取最大值
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="condition">条件</param>
        /// <param name="isWrite">是否为写连接</param>
        /// <returns>最大值</returns>
        public Task<TReturn> MaxAsync<TProperty, TReturn>(Expression<Func<T, TProperty>> expression, Expression<Func<T, bool>> condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryMaxSqlQuery(expression, TableName, condition, dbType: WriterDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefaultAsync<TReturn>(query);
        }

        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        public T GetInfo(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryTopSqlQuery(condition, TableName, topCount: 1, dbType: WriterDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefault<T>(query);
        }

        /// <summary>
        /// 异步获取单个对象
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        public Task<T> GetInfoAsync(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryTopSqlQuery(condition, TableName, topCount: 1, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefaultAsync<T>(query);
        }

        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        public T GetInfo(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryTopSqlQuery(condition, TableName, topCount: 1, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefault<T>(query);
        }

        /// <summary>
        /// 异步获取单个对象
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        public Task<T> GetInfoAsync(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryTopSqlQuery(condition, TableName, topCount: 1, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefaultAsync<T>(query);
        }

        /// <summary>
        /// 获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        public TDto GetDto<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryTopSqlQuery<TDto>(condition, TableName, topCount: 1, ignoreFields: ignoreFields, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefault<TDto>(query);
        }

        /// <summary>
        /// 异步获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        public Task<TDto> GetDtoAsync<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryTopSqlQuery<TDto>(condition, TableName, topCount: 1, ignoreFields: ignoreFields, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefaultAsync<TDto>(query);
        }

        /// <summary>
        /// 获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        public TDto GetDto<TDto>(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryTopSqlQuery<TDto, T>(condition, TableName, topCount: 1, ignoreFields: ignoreFields, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefault<TDto>(query);
        }

        /// <summary>
        /// 异步获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        public Task<TDto> GetDtoAsync<TDto>(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryTopSqlQuery<TDto, T>(condition, TableName, topCount: 1, ignoreFields: ignoreFields, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefaultAsync<TDto>(query);
        }

        /// <summary>
        /// 获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="sqlQuery">SqlQuery</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        protected TDto GetDto<TDto>(SqlQuery sqlQuery, bool isWrite = false)
        {
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefault<TDto>(sqlQuery);
        }

        /// <summary>
        /// 异步获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="sqlQuery">SqlQuery</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        protected Task<TDto> GetDtoAsync<TDto>(SqlQuery sqlQuery, bool isWrite = false)
        {
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefaultAsync<TDto>(sqlQuery);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        public IEnumerable<T> QueryList(bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery(null, TableName, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).Query<T>(query);
        }

        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        public Task<IEnumerable<T>> QueryListAsync(bool isWrite = false)
        {
            return Task.FromResult(QueryList(isWrite: isWrite));
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        public IEnumerable<T> QueryList(object condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery(condition, TableName, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).Query<T>(query);
        }

        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        public Task<IEnumerable<T>> QueryListAsync(object condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery(condition, TableName, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QueryAsync<T>(query);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        public IEnumerable<T> QueryList(Expression<Func<T, bool>> condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery(condition, TableName, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).Query<T>(query);
        }

        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        public Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery(condition, TableName, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QueryAsync<T>(query);
        }

        /// <summary>
        /// 查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        public IEnumerable<TDto> QueryList<TDto>(string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<TDto>(null, TableName, ignoreFields: ignoreFields, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).Query<TDto>(query);
        }

        /// <summary>
        /// 异步查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        public Task<IEnumerable<TDto>> QueryListAsync<TDto>(string[] ignoreFields = null, bool isWrite = false)
        {
            return Task.FromResult(QueryList<TDto>(ignoreFields: ignoreFields, isWrite: isWrite));
        }

        /// <summary>
        /// 查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        public IEnumerable<TDto> QueryList<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<TDto>(condition, TableName, ignoreFields: ignoreFields, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).Query<TDto>(query);
        }

        /// <summary>
        /// 异步查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        public Task<IEnumerable<TDto>> QueryListAsync<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<TDto>(condition, TableName, ignoreFields: ignoreFields, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QueryAsync<TDto>(query);
        }

        /// <summary>
        /// 查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        public IEnumerable<TDto> QueryList<TDto>(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<TDto, T>(condition, TableName, ignoreFields: ignoreFields, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).Query<TDto>(query);
        }

        /// <summary>
        /// 异步查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        public Task<IEnumerable<TDto>> QueryListAsync<TDto>(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<TDto, T>(condition, TableName, ignoreFields: ignoreFields, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).QueryAsync<TDto>(query);
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>范总数量</returns>
        public int QueryCount(object condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryCountSqlQuery(condition, TableName, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).ExecuteScalar<int>(query);
        }

        /// <summary>
        /// 异步查询数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>范总数量</returns>
        public Task<int> QueryCountAsync(object condition, bool isWrite = false)
        {
            return Task.FromResult(QueryCount(condition, isWrite: isWrite));
        }

        /// <summary>
        /// 异步查询数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>范总数量</returns>
        public Task<int> QueryCountAsync(Expression<Func<T, bool>> condition, bool isWrite = false)
        {
            return Task.FromResult(QueryCount(condition, isWrite: isWrite));
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>范总数量</returns>
        public int QueryCount(Expression<Func<T, bool>> condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryCountSqlQuery(condition, TableName, dbType: isWrite ? WriterDataType : ReaderDataType);
            return GetDataAccess(isWrite: isWrite).ExecuteScalar<int>(query);
        }

        /// <summary>
        /// 分页查询（目前只支持MSSQLServer）
        /// </summary>
        /// <typeparam name="TModel">分页结果类型</typeparam>
        /// <param name="selectColumn">查询的列</param>
        /// <param name="selectTable">查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">每页个数</param>
        /// <param name="cmdParms">参数值</param>
        /// <returns>分页结果</returns>
        public IPageResult<TModel> QueryPageList<TModel>(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, object cmdParms = null)
        {
            int totalCount = QueryCount(selectTable, where, cmdParms: cmdParms);
            var dataList = PageQuery<TModel>(selectColumn, selectTable, where, order, pageIndex, pageSize, cmdParms: cmdParms);
            return new PageResult<TModel>(pageIndex, pageSize, totalCount, dataList);
        }

        /// <summary>
        /// 异步分页查询（目前只支持MSSQLServer）
        /// </summary>
        /// <typeparam name="TModel">分页结果类型</typeparam>
        /// <param name="selectColumn">查询的列</param>
        /// <param name="selectTable">查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">每页个数</param>
        /// <param name="cmdParms">参数值</param>
        /// <returns>分页结果</returns>
        public Task<IPageResult<TModel>> QueryPageListAsync<TModel>(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, object cmdParms = null)
        {
            return Task.FromResult(QueryPageList<TModel>(selectColumn, selectTable, where, order, pageIndex, pageSize, cmdParms: cmdParms));
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="selectTable">查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="isWrite">是否为写</param>
        /// <returns>数量</returns>
        protected int QueryCount(string selectTable, string where, object cmdParms = null, bool isWrite = false)
        {
            StringBuilder selectSQL = new StringBuilder();
            selectSQL.Append(string.Format("SELECT COUNT(0) FROM {0} ", selectTable));
            if (!string.IsNullOrWhiteSpace(where)) selectSQL.Append(string.Format(" WHERE {0}", where));
            SqlQuery query = new SqlQuery(selectSQL.ToString(), cmdParms);
            return GetDataAccess(isWrite: isWrite).ExecuteScalar<int>(query);
        }

        /// <summary>
        /// 异步查询数量
        /// </summary>
        /// <param name="selectTable">查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="cmdParms">参数</param>
        /// <param name="isWrite">是否为写</param>
        /// <returns>数量</returns>
        protected Task<int> QueryCountAsync(string selectTable, string where, object cmdParms = null, bool isWrite = false)
        {
            return Task.FromResult(QueryCount(selectTable, where, cmdParms, isWrite: isWrite));
        }

        /// <summary>
        /// 查询传输对象
        /// </summary>
        /// <typeparam name="TDto">茶树对象类型</typeparam>
        /// <param name="query">查询信息</param>
        /// <param name="isWrite">是否为写</param>
        /// <returns>传输对象</returns>
        protected TDto SingleOrDefault<TDto>(SqlQuery query, bool isWrite = false)
        {
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefault<TDto>(query);
        }

        /// <summary>
        /// 异步查询传输对象
        /// </summary>
        /// <typeparam name="TDto">茶树对象类型</typeparam>
        /// <param name="query">查询信息</param>
        /// <param name="isWrite">是否为写</param>
        /// <returns>传输对象</returns>
        protected Task<TDto> SingleOrDefaultAsync<TDto>(SqlQuery query, bool isWrite = false)
        {
            return GetDataAccess(isWrite: isWrite).QuerySingleOrDefaultAsync<TDto>(query);
        }

        /// <summary>
        /// 查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">茶树对象类型</typeparam>
        /// <param name="query">查询信息</param>
        /// <param name="isWrite">是否为写</param>
        /// <returns>传输对象列表</returns>
        protected IEnumerable<TDto> QueryList<TDto>(SqlQuery query, bool isWrite = false)
        {
            return GetDataAccess(isWrite: isWrite).Query<TDto>(query);
        }

        /// <summary>
        /// 异步查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">查询对象类型</typeparam>
        /// <param name="query">查询信息</param>
        /// <param name="isWrite">是否为写</param>
        /// <returns>传输对象列表</returns>
        protected Task<IEnumerable<TDto>> QueryListAsync<TDto>(SqlQuery query, bool isWrite = false)
        {
            return GetDataAccess(isWrite: isWrite).QueryAsync<TDto>(query);
        }

        /// <summary>
        /// 查询Table
        /// </summary>
        /// <param name="query">查询信息</param>
        /// <param name="isWrite">是否为写</param>
        /// <returns>Table</returns>
        protected DataTable QueryTable(SqlQuery query, bool isWrite = false)
        {
            return GetDataAccess(isWrite: isWrite).QueryTable(query);
        }

        /// <summary>
        /// 异步查询Table
        /// </summary>
        /// <param name="query">查询信息</param>
        /// <param name="isWrite">是否为写</param>
        /// <returns>Table</returns>
        protected Task<DataTable> QueryTableAsync(SqlQuery query, bool isWrite = false)
        {
            return GetDataAccess(isWrite: isWrite).QueryTableAsync(query);
        }

        /// <summary>
        /// 分页查询(目前只支持MSSQLServer)
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="selectColumn">需要查询的指定字段(多个之间用逗号隔开)</param>
        /// <param name="selectTable">需要查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">一页显示条目</param>
        /// <param name="cmdParms">条件值</param>
        /// <returns>分页查询结果</returns>
        protected IEnumerable<TModel> PageQuery<TModel>(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, object cmdParms = null)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryPageSqlQuery(selectColumn, selectTable, where, order, pageIndex, pageSize, dbType: ReaderDataType, cmdParms: cmdParms);
            return GetDataAccess(isWrite: false).Query<TModel>(query);
        }

        /// <summary>
        /// 异步分页查询(目前只支持MSSQLServer)
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="selectColumn">需要查询的指定字段(多个之间用逗号隔开)</param>
        /// <param name="selectTable">需要查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">一页显示条目</param>
        /// <param name="cmdParms">条件值</param>
        /// <returns>分页查询结果</returns>
        protected Task<IEnumerable<TModel>> PageQueryAsync<TModel>(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, object cmdParms = null)
        {
            return Task.FromResult(PageQuery<TModel>(selectColumn, selectTable, where, order, pageIndex, pageSize, cmdParms: cmdParms));
        }
    }
}