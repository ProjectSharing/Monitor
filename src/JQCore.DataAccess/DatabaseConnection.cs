namespace JQCore.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DatabaseConnection.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：连接信息
    /// 创建标识：yjq 2017/9/5 19:09:16
    /// </summary>
    public struct DatabaseConnection
    {
        private string _connectionString;
        private DatabaseType _databaseType;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType
        {
            get
            {
                return _databaseType;
            }
            set
            {
                _databaseType = value;
            }
        }
    }
}