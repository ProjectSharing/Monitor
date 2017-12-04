using JQCore.Configuration;
using System;

namespace JQCore.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DBSettings.cs
    /// 类属性：局部类（静态）
    /// 类功能描述：设置数据库属性信息
    /// 创建标识：yjq 2017/9/5 19:11:41
    /// </summary>
    internal static class DBSettings
    {
        /// <summary>
        /// 根据数据库类型和连接信息创建DatabaseProperty
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="connection">连接信息</param>
        /// <returns>DatabaseProperty</returns>
        public static DatabaseProperty CreateDatabaseProperty(DatabaseType databaseType, string connection)
        {
            DatabaseConnection dbConnection = new DatabaseConnection()
            {
                ConnectionString = connection,
                DatabaseType = databaseType
            };
            return new DatabaseProperty(dbConnection, dbConnection);
        }

        /// <summary>
        /// 根据配置名字获取数据库属性信息
        /// </summary>
        /// <param name="name">配置名字</param>
        /// <returns>数据库属性信息</returns>
        public static DatabaseProperty GetDatabaseProperty(string name)
        {
            DatabaseConnection reader = GetDbConnection(name + ":Reader");
            DatabaseConnection writer = GetDbConnection(name + ":Writer");
            return new DatabaseProperty(reader, writer);
        }

        /// <summary>
        /// 根据配置名字获取连接信息
        /// </summary>
        /// <param name="connectionSettingName">配置名字</param>
        /// <returns>连接信息</returns>
        private static DatabaseConnection GetDbConnection(string connectionSettingName)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.ConnectionString = ConfigurationManage.GetValue($"ConnectionStrings:{connectionSettingName}:ConnectionString");
            dbConnection.DatabaseType = ConfigurationManage.GetValue<DatabaseType>($"ConnectionStrings:{connectionSettingName}:DatabaseType");
            return dbConnection;
        }

        /// <summary>
        /// 根据提供程序名称属性获取对应数据库类型
        /// </summary>
        /// <param name="providerName">提供程序名称属性</param>
        /// <returns>数据库类型</returns>
        private static DatabaseType GetDbType(string providerName)
        {
            DatabaseType dataType = default(DatabaseType);
            switch (providerName)
            {
                case DbClientType.DB_CLINET_MSSQL:
                    dataType = DatabaseType.MSSQLServer;
                    break;

                case DbClientType.DB_CLINET_MYSQL:
                    dataType = DatabaseType.MySql;
                    break;

                case DbClientType.DB_CLINET_ORACLE:
                    dataType = DatabaseType.Oracle;
                    break;

                case DbClientType.DB_CLINET_POSTGRESQL:
                    dataType = DatabaseType.PostgreSqlClient;
                    break;

                default:
                    throw new ArgumentNullException(providerName, "未找到该数据库类型.");
            }
            return dataType;
        }
    }
}