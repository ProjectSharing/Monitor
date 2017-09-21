using Microsoft.Extensions.DependencyInjection;

namespace JQCore.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DBServiceCollectionExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务扩展类
    /// 创建标识：yjq 2017/9/5 20:37:33
    /// </summary>
    public static class DBServiceCollectionExtension
    {
        /// <summary>
        /// 使用默认的数据库的底层
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UseDefaultDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IDataAccessFactory, DataAccessFactory>();
            return services;
        }
    }
}