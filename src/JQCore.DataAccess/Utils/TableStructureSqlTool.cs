using System;

namespace JQCore.DataAccess.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：TableStructureSqlTool.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/12/4 11:43:26
    /// </summary>
    public static partial class SqlQueryUtil
    {
        #region 获取查询Table列表的sql语句

        /// <summary>
        /// 获取查询Table列表的sql语句
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="databaseName">数据库名字</param>
        /// <param name="tablePrefix">表前缀</param>
        /// <returns>查询Table列表的sql语句</returns>
        public static string GetQueryTableListSql(DatabaseType databaseType, string databaseName = null, string tablePrefix = null)
        {
            string selectTableSql = string.Empty;
            switch (databaseType)
            {
                case DatabaseType.MSSQLServer:
                    selectTableSql = GetSqlServerTableListSql(tablePrefix: tablePrefix);
                    break;

                case DatabaseType.MySql:
                    selectTableSql = GetQueryMySqlTableListSql(databaseName, tablePrefix: tablePrefix);
                    break;

                default:
                    throw new NotSupportedException($"未实现【{databaseType.ToString()}】sqlTable查询语句");
            }
            return selectTableSql;
        }

        /// <summary>
        /// 获取MYSQL的查询数据库中表列表的语句
        /// </summary>
        /// <param name="databaseName">数据库名字</param>
        /// <param name="tablePrefix">表前缀</param>
        /// <returns>MYSQL的查询数据库中表列表的语句</returns>
        private static string GetQueryMySqlTableListSql(string databaseName, string tablePrefix = null)
        {
            string tableSql = "SELECT TABLE_NAME TableName,TABLE_COMMENT Description FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = '" + databaseName + "' ";
            if (!string.IsNullOrWhiteSpace(tablePrefix))
            {
                tableSql += string.Format(" AND TABLE_NAME like '{0}%'", tablePrefix);
            }
            return tableSql;
        }

        /// <summary>
        /// 获取SqlServer的查询数据库中表列表的语句
        /// </summary>
        /// <param name="tablePrefix">表前缀</param>
        /// <returns>SqlServer的查询数据库中表列表的语句</returns>
        private static string GetSqlServerTableListSql(string tablePrefix = null)
        {
            string tableSql = @"SELECT
                                CASE WHEN A.COLORDER=1 THEN D.NAME ELSE '' END TableName,
                                CASE WHEN A.COLORDER=1 THEN ISNULL(F.VALUE,'') ELSE '' END Description
                                FROM
                                SYSCOLUMNS A
                                INNER JOIN
                                SYSOBJECTS D
                                ON
                                A.ID=D.ID  AND D.XTYPE='U' AND  D.NAME<>'DTPROPERTIES'
                                LEFT JOIN
                                sys.extended_properties F
                                ON
                                D.ID=F.major_id AND F.minor_id=0
                                where a.colorder=1";
            if (!string.IsNullOrWhiteSpace(tablePrefix))
            {
                tableSql += string.Format(" AND D.NAME like '{0}%'", tablePrefix);
            }
            tableSql += " ORDER BY  A.ID,A.COLORDER;";
            return tableSql;
        }

        #endregion 获取查询Table列表的sql语句

        #region 获取查询列信息的SQL语句

        /// <summary>
        /// 获取查询列信息的SQL语句
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="databaseName">数据库名字</param>
        /// <param name="tablePrefix">表前缀</param>
        /// <returns>查询列信息的SQL语句</returns>
        public static string GetQueryTableColumnListSql(DatabaseType databaseType, string databaseName = null, string tablePrefix = null)
        {
            string selectTableColumnSql = string.Empty;
            switch (databaseType)
            {
                case DatabaseType.MSSQLServer:
                    selectTableColumnSql = GetQuerySqlServerTableColumnListSql(tablePrefix: tablePrefix);
                    break;

                case DatabaseType.MySql:
                    selectTableColumnSql = GetMySqlTableColumnListSql(databaseName, tablePrefix: tablePrefix);
                    break;

                default:
                    throw new NotSupportedException($"未实现【{databaseType.ToString()}】TableColumnListSql查询语句");
            }
            return selectTableColumnSql;
        }

        /// <summary>
        /// 获取MySql的查询表列信息Sql
        /// </summary>
        /// <param name="databaseName">数据库名字</param>
        /// <param name="tablePrefix">表前缀</param>
        /// <returns>MySql的查询表列信息Sql</returns>
        private static string GetMySqlTableColumnListSql(string databaseName, string tablePrefix = null)
        {
            string sql = @"SELECT a.TABLE_NAME AS TableName,0 AS TableId,b.COLUMN_NAME AS ColumnName,b.DATA_TYPE AS DataType,b.CHARACTER_MAXIMUM_LENGTH AS MaxLength,b.COLUMN_COMMENT AS ColumnDescription,b.COLUMN_DEFAULT AS ColumnDefaultValue,case b.IS_NULLABLE when  'NO' THEN 0 ELSE 1  END IsNullable,CASE b.COLUMN_KEY WHEN 'PRI' THEN 1  ELSE 0 END IsKey, CASE b.EXTRA WHEN 'auto_increment' THEN 1 ELSE 0 END IsIdentity  FROM information_schema. TABLES a LEFT JOIN information_schema. COLUMNS b ON a.table_name = b.TABLE_NAME AND b.table_schema='" + databaseName + "'  WHERE a.table_schema = '" + databaseName + "'";
            if (!string.IsNullOrWhiteSpace(tablePrefix))
            {
                sql += string.Format(" AND a.TABLE_NAME like '{0}%'", tablePrefix);
            }
            sql += " ORDER BY a.table_name";
            return sql;
        }

        /// <summary>
        /// 获取Sqlserver查询表列信息Sql
        /// </summary>
        /// <param name="tablePrefix">表前缀</param>
        /// <returns>Sqlserver查询表列信息Sql</returns>
        private static string GetQuerySqlServerTableColumnListSql(string tablePrefix = null)
        {
            string sql = @"SELECT  Sysobjects.name AS TableName ,
								syscolumns.Id  AS TableId,
								syscolumns.name AS ColumnName ,
								systypes.name AS DataType ,
								syscolumns.length AS MaxLength ,
								sys.extended_properties.[value] AS ColumnDescription ,
								syscomments.text AS ColumnDefaultValue ,
								syscolumns.isnullable AS IsNullable,
                                (case when exists(SELECT 1 FROM sysobjects where xtype= 'PK' and name in (
                                SELECT name FROM sysindexes WHERE indid in(
                                SELECT indid FROM sysindexkeys WHERE id = syscolumns.id AND colid=syscolumns.colid
                                ))) then 1 else 0 end) as IsKey,
                                COLUMNPROPERTY(syscolumns.id, syscolumns.name, 'IsIdentity') IsIdentity

								FROM    syscolumns
								INNER JOIN systypes ON syscolumns.xtype = systypes.xtype
								LEFT JOIN sysobjects ON syscolumns.id = sysobjects.id
								LEFT OUTER JOIN sys.extended_properties ON ( sys.extended_properties.minor_id = syscolumns.colid
																			 AND sys.extended_properties.major_id = syscolumns.id
																		   )
								LEFT OUTER JOIN syscomments ON syscolumns.cdefault = syscomments.id
								WHERE   syscolumns.id IN ( SELECT   id
												   FROM     SYSOBJECTS
												   WHERE    xtype in( 'U','V') )
								AND ( systypes.name <> 'sysname' )  AND systypes.name<>'geometry' AND systypes.name<>'geography'";// AND Sysobjects.name='" + tableName + "'
            if (!string.IsNullOrWhiteSpace(tablePrefix))
            {
                sql += string.Format(" AND Sysobjects.name like '{0}%'", tablePrefix);
            }
            sql += "  ORDER BY syscolumns.colid;";
            return sql;
        }

        #endregion 获取查询列信息的SQL语句
    }
}