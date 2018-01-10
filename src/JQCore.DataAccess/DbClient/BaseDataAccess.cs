using JQCore.DataAccess.Utils;
using JQCore.Utils;
using JQCore.Web;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JQCore.DataAccess.DbClient
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BaseDataAccess.cs
    /// 类属性：抽象类（非静态）
    /// 类功能描述：数据库访问基础类
    /// 创建标识：yjq 2017/9/5 19:32:55
    /// </summary>
    public abstract class BaseDataAccess : SelfDisposable
    {
        /// <summary>
        /// 超时时间
        /// </summary>
        protected int _cmdTimeout = 3000;

        /// <summary>
        /// 连接
        /// </summary>
        protected IDbConnection _conn;

        /// <summary>
        /// 事务
        /// </summary>
        protected IDbTransaction _tran;

        /// <summary>
        /// 数据库类型
        /// </summary>
        protected DatabaseType _dbType;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="dbProperty">数据库属性信息</param>
        /// <param name="isWrite">是否写连接（默认使用读连接）</param>
        public BaseDataAccess(DatabaseProperty dbProperty, bool isWrite = false)
        {
            EnsureUtil.NotNull(dbProperty, "dbProperty不能为空!");
            DatabaseConnection dbConnection = isWrite ? dbProperty.Writer : dbProperty.Reader;
            _conn = CreateConnection(dbConnection);
        }

        #region .protery

        /// <summary>
        /// 连接信息
        /// </summary>
        public virtual IDbConnection Connection
        {
            get
            {
                return _conn;
            }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        protected DatabaseType DatabaseTyoe
        {
            get
            {
                return _dbType;
            }
        }

        /// <summary>
        /// 连接是否已关闭
        /// </summary>
        public virtual bool IsClosed
        {
            get
            {
                if (_conn == null)
                {
                    return true;
                }
                return (_conn.State == ConnectionState.Closed || _conn.State == ConnectionState.Broken);
            }
        }

        /// <summary>
        /// 超时时间
        /// </summary>
        public virtual int CommandTimeOut
        {
            get
            {
                return _cmdTimeout;
            }
            set
            {
                _cmdTimeout = value;
            }
        }

        /// <summary>
        /// 事务
        /// </summary>
        public virtual IDbTransaction Tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        #endregion .protery

        /// <summary>
        /// 打开连接
        /// </summary>
        public virtual void Open()
        {
            ConnIsNotNull();
            if (IsClosed)
            {
                _conn.Open();
            }
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public virtual IDbTransaction BeginTran()
        {
            ConnIsNotNull();
            Open();
            return _tran = Connection.BeginTransaction();
        }

        /// <summary>
        /// 开启一个事务
        /// </summary>
        /// <param name="il">隔离级别</param>
        /// <returns></returns>
        public virtual IDbTransaction BeginTran(IsolationLevel il)
        {
            ConnIsNotNull();
            Open();
            return _tran = Connection.BeginTransaction(il);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public virtual void CommitTran()
        {
            Tran?.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public virtual void RollbackTran()
        {
            Tran?.Rollback();
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public virtual void Close()
        {
            Tran?.Dispose();
            if (!IsClosed)
            {
                Connection.Close();
            }
            Connection?.Dispose();
        }

        /// <summary>
        /// 根据连接信息创建连接对象
        /// </summary>
        /// <param name="dbConnection">连接信息</param>
        /// <returns>连接对象</returns>
        private IDbConnection CreateConnection(DatabaseConnection dbConnection)
        {
            IDbConnection conn;
            _dbType = dbConnection.DatabaseType;
            switch (dbConnection.DatabaseType)
            {
                case DatabaseType.MSSQLServer:
                    conn = new SqlConnection(dbConnection.ConnectionString);
                    break;

                case DatabaseType.MySql:
                    conn = new MySqlConnection(dbConnection.ConnectionString);
                    break;

                default:
                    throw new NotSupportedException("DatabaseType NotSupported");
            }
            return conn;
        }

        /// <summary>
        /// 连接不为空
        /// </summary>
        private void ConnIsNotNull()
        {
            EnsureUtil.NotNull(_conn, "conn不能为空!");
        }

        protected override void DisposeCode()
        {
            Close();
        }

        /// <summary>
        /// 对sql语句进行处理
        /// </summary>
        /// <param name="q"></param>
        protected void PrepareSqlQuery(SqlQuery q)
        {
            if (q.CommandType == CommandType.Text)
            {
                if (WebHttpContext.IsHaveHttpContext)
                {
                    string text = WebHttpContext.AbsoluteUrl;
                    if (text.Contains("?"))
                    {
                        text = text.Remove(text.IndexOf("?"));
                    }
                    q.CommandText = q.CommandText + "/* URL:" + text + " */";
                }
                else
                {
                    q.CommandText = q.CommandText + "/* Location:" + AppDomain.CurrentDomain.FriendlyName + " */";
                }
            }
        }

        /// <summary>
        /// 批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">列表信息</param>
        /// <param name="tableName">表名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        public void BulkInsert<T>(List<T> list, string tableName, string[] ignoreFields = null)
        {
            var dataTable = list.ToTable(ignoreFields: ignoreFields);
            BulkInsert(dataTable, tableName);
        }

        /// <summary>
        /// 异步批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">列表信息</param>
        /// <param name="tableName">表名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <returns>Task</returns>
        public Task BulkInsertAsync<T>(List<T> list, string tableName, string[] ignoreFields = null)
        {
            var dataTable = list.ToTable(ignoreFields: ignoreFields);
            return BulkInsertAsync(dataTable, tableName);
        }

        /// <summary>
        /// 批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="tableName">表名字</param>
        public void BulkInsert(DataTable table, string tableName)
        {
            SqlMonitorUtil.Monitor(() =>
            {
                if (table != null && DatabaseTyoe == DatabaseType.MSSQLServer)
                {
                    Open();
                    using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(Connection as SqlConnection, SqlBulkCopyOptions.Default, Tran as SqlTransaction))
                    {
                        sqlbulkcopy.BatchSize = table.Rows.Count;
                        sqlbulkcopy.DestinationTableName = tableName;
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            sqlbulkcopy.ColumnMappings.Add(table.Columns[i].ColumnName, table.Columns[i].ColumnName);
                        }
                        sqlbulkcopy.WriteToServer(table.CreateDataReader());
                    }
                }
                else if (table != null && DatabaseTyoe == DatabaseType.MySql)
                {
                    string tmpPath = System.IO.Path.GetTempFileName();
                    try
                    {
                        string csv = table.ToCsv();
                        System.IO.File.WriteAllText(tmpPath, csv);
                        Open();
                        MySqlBulkLoader bulk = new MySqlBulkLoader(Connection as MySqlConnection)
                        {
                            FieldTerminator = ",",
                            FieldQuotationCharacter = '"',
                            EscapeCharacter = '"',
                            LineTerminator = "\r\n",
                            FileName = tmpPath,
                            NumberOfLinesToSkip = 0,
                            TableName = tableName,
                        };
                        bulk.Columns.AddRange(table.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToList());
                        int insertCount = bulk.Load();
                    }
                    finally
                    {
                        FileUtil.DeleteFile(tmpPath);
                    }
                }
                else
                {
                    throw new NotSupportedException(DatabaseTyoe.ToString());
                }
            }, dbType: DatabaseTyoe.ToString(), memberName: "BaseDataAccess-BulkInsert");
        }

        /// <summary>
        /// 异步批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="tableName">表名字</param>
        /// <returns>Task</returns>
        public Task BulkInsertAsync(DataTable table, string tableName)
        {
            return SqlMonitorUtil.MonitorAsync(async () =>
            {
                if (table != null && DatabaseTyoe == DatabaseType.MSSQLServer)
                {
                    Open();
                    using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(Connection as SqlConnection, SqlBulkCopyOptions.Default, Tran as SqlTransaction))
                    {
                        sqlbulkcopy.BatchSize = table.Rows.Count;
                        sqlbulkcopy.DestinationTableName = tableName;
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            sqlbulkcopy.ColumnMappings.Add(table.Columns[i].ColumnName, table.Columns[i].ColumnName);
                        }
                        await sqlbulkcopy.WriteToServerAsync(table.CreateDataReader());
                    }
                }
                else if (table != null && DatabaseTyoe == DatabaseType.MySql)
                {
                    string tmpPath = System.IO.Path.GetTempFileName();
                    try
                    {
                        string csv = table.ToCsv();
                        System.IO.File.WriteAllText(tmpPath, csv);
                        Open();
                        MySqlBulkLoader bulk = new MySqlBulkLoader(Connection as MySqlConnection)
                        {
                            FieldTerminator = ",",
                            FieldQuotationCharacter = '"',
                            EscapeCharacter = '"',
                            LineTerminator = "\r\n",
                            FileName = tmpPath,
                            NumberOfLinesToSkip = 0,
                            TableName = tableName,
                        };
                        bulk.Columns.AddRange(table.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToList());
                        int insertCount = await bulk.LoadAsync();
                    }
                    finally
                    {
                        FileUtil.DeleteFile(tmpPath);
                    }
                }
                else
                {
                    throw new NotSupportedException(DatabaseTyoe.ToString());
                }
            }, dbType: DatabaseTyoe.ToString(), memberName: "BaseDataAccess-BulkInsert");
        }
    }
}