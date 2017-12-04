using JQCore.DataAccess.DbClient;
using System;

namespace JQCore.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IDataAccessFactory.cs
    /// 接口属性：公共
    /// 类功能描述：IDataAccessFactory接口
    /// 创建标识：yjq 2017/9/5 20:34:40
    /// </summary>
    public interface IDataAccessFactory : IDisposable
    {
        /// <summary>
        /// 获取一个数据操作类
        /// </summary>
        /// <param name="configName">配置文件名字</param>
        /// <param name="isWriter">是否为写连接，不是则为读连接，默认为写连接</param>
        /// <returns>数据操作</returns>
        IDataAccess GetDataAccess(string configName, bool isWriter = true);

        /// <summary>
        ///  获取数据库访问接口(返回对象需要自己处理释放)
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="connetion">连接信息</param>
        /// <param name="isWriter">是否为写</param>
        /// <returns>数据库访问接口</returns>
        IDataAccess GetDataAccess(DatabaseType databaseType, string connetion, bool isWriter = true);
    }
}