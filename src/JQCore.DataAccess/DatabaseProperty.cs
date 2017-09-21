namespace JQCore.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DatabaseProperty.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：数据库属性信息
    /// 创建标识：yjq 2017/9/5 19:10:19
    /// </summary>
    public sealed class DatabaseProperty
    {
        private DatabaseConnection _reader;
        private DatabaseConnection _writer;

        /// <summary>
        /// 读链接信息
        /// </summary>
        public DatabaseConnection Reader
        {
            get { return _reader; }
        }

        /// <summary>
        /// 写链接信息
        /// </summary>
        public DatabaseConnection Writer
        {
            get { return _writer; }
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="reader">读链接信息</param>
        /// <param name="writer">写链接信息</param>
        public DatabaseProperty(DatabaseConnection reader, DatabaseConnection writer)
        {
            _reader = reader;
            _writer = writer;
        }
    }
}