namespace JQCore.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DbClientType.cs
    /// 类属性：内部类（静态）
    /// 类功能描述：数据库连接类型
    /// 创建标识：yjq 2017/9/5 18:48:57
    /// </summary>
    internal static class DbClientType
    {
        /// <summary>
        /// sqlservcer的连接类型
        /// </summary>
        public const string DB_CLINET_MSSQL = "System.Data.SqlClient";

        /// <summary>
        /// Oracle的连接类型
        /// </summary>
        public const string DB_CLINET_ORACLE = "System.Data.OracleClien";

        /// <summary>
        /// MySql的连接类型
        /// </summary>
        public const string DB_CLINET_MYSQL = "MySql.Data.MySqlClient";

        /// <summary>
        /// PostgreSql的连接类型
        /// </summary>
        public const string DB_CLINET_POSTGRESQL = "MySql.Data.PostgreSqlClient";
    }
}