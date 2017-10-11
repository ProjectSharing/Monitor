using JQCore.Configuration;

namespace JQCore.Redis
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DefaultLockOptionProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/2 22:28:24
    /// </summary>
    public sealed class DefaultLockOptionProvider : ILockOptionProvider
    {
        public RedisCacheOption RedisCacheOption
        {
            get
            {
                var connectionStr = ConfigurationManage.GetValue("Redis:Lock:Default:ConnectionString");
                var databaseId = ConfigurationManage.GetValue<int>("Redis:Lock:Default:DatabaseId");
                var prefix = ConfigurationManage.GetValue("Redis:Lock:Default:Prefix");
                var namespaceSplitSymbol = ConfigurationManage.GetValue("Redis:Lock:Default:NamespaceSplitSymbol");
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