using JQCore.Configuration;
using JQCore.Redis;

namespace Monitor.Cache
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RedisOptionProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/3 14:01:25
    /// </summary>
    public sealed class RedisOptionProvider
    {
        public static RedisCacheOption DefaultOption
        {
            get
            {
                var connectionStr = ConfigurationManage.GetValue("Redis:Web:Default:ConnectionString");
                var databaseId = ConfigurationManage.GetValue<int>("Redis:Web:Default:DatabaseId");
                var prefix = ConfigurationManage.GetValue("Redis:Web:Default:Prefix");
                var namespaceSplitSymbol = ConfigurationManage.GetValue("Redis:Web:Default:NamespaceSplitSymbol");
                return new RedisCacheOption
                {
                    ConnectionString = connectionStr,
                    DatabaseId = databaseId,
                    NamespaceSplitSymbol = namespaceSplitSymbol,
                    Prefix = prefix
                };
            }
        }
    }
}