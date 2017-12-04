using JQCore.DataAccess.DbClient;
using System.Collections.Generic;

namespace JQCore.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DataAccessFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/5 20:35:42
    /// </summary>
    public sealed class DataAccessFactory : SelfDisposable, IDataAccessFactory
    {
        /// <summary>
        /// 数据库访问单个请求单列
        /// </summary>
        private Dictionary<string, IDataAccess> _dataAccessCache = new Dictionary<string, IDataAccess>();

        /// <summary>
        /// 获取数据库访问接口(当获取读连接时，发现已经有写连接了则返回写连接)
        /// </summary>
        /// <param name="configName">配置项名字(config的名字)</param>
        /// <param name="isWriter">是否为写连接</param>
        /// <returns>数据库访问接口</returns>
        public IDataAccess GetDataAccess(string configName, bool isWriter = true)
        {
            string key = (isWriter == true) ? $"{configName}_1" : $"{configName}_0";

            //当获取读连接时，发现已经有写连接了则返回写连接
            IDataAccess dataAccess = null;
            if (!isWriter)
            {
                if (_dataAccessCache.ContainsKey($"{configName}_1"))
                {
                    dataAccess = _dataAccessCache[$"{configName}_1"];
                    if (dataAccess == null)
                    {
                        dataAccess = CreateDataAccess(configName, isWriter: true);
                        _dataAccessCache[$"{configName}_1"] = dataAccess;
                    }
                    return dataAccess;
                }
            }
            if (_dataAccessCache.ContainsKey(key))
            {
                dataAccess = _dataAccessCache[key];
                if (dataAccess == null)
                {
                    dataAccess = CreateDataAccess(configName, isWriter: isWriter);
                    _dataAccessCache[key] = dataAccess;
                }
                return dataAccess;
            }
            else
            {
                return _dataAccessCache[key] = CreateDataAccess(configName, isWriter: isWriter);
            }
        }

        /// <summary>
        ///  获取数据库访问接口
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="connetion">连接信息</param>
        /// <param name="isWriter">是否为写</param>
        /// <returns>数据库访问接口</returns>
        public IDataAccess GetDataAccess(DatabaseType databaseType, string connetion, bool isWriter = true)
        {
            var dbProperty = DBSettings.CreateDatabaseProperty(databaseType, connetion);
            return new SqlDataAccess(dbProperty, isWriter);
        }

        /// <summary>
        /// 常见数据库访问类
        /// </summary>
        /// <param name="configName">配置项名字(config的名字)</param>
        /// <param name="isWriter">是否为写连接</param>
        /// <returns>数据库访问接口</returns>
        private IDataAccess CreateDataAccess(string configName, bool isWriter = true)
        {
            var dbProperty = DBSettings.GetDatabaseProperty(configName);
            return new SqlDataAccess(dbProperty, isWriter);
        }

        protected override void DisposeCode()
        {
            // LogUtil.Info("DataAccessFactory:执行释放方法");
            foreach (var item in _dataAccessCache)
            {
                item.Value?.Close();
                item.Value?.Dispose();
            }
            _dataAccessCache?.Clear();
        }
    }
}