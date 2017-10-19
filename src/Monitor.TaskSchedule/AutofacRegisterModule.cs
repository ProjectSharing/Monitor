using Autofac;
using JQCore.DataAccess;
using JQCore.Dependency;
using JQCore.Serialization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;

namespace Monitor.TaskSchedule
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AutofacRegisterModule.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/16 20:22:23
    /// </summary>
    public class AutofacRegisterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var repositoryAssembly = Assembly.Load("Monitor.Repository");
            var domainServiceAssembly = Assembly.Load("Monitor.DomainService");
            var cacheAssembly = Assembly.Load("Monitor.Cache");
            var userApplicationAssembly = Assembly.Load("Monitor.Application");
            var unitOfWorkAssembly = Assembly.Load("Monitor.UnitOfWork");

            builder.RegisterAssembly(repositoryAssembly, m => m.Namespace != null && m.Namespace.StartsWith("Monitor.Repository.Implement") && m.Name.EndsWith("Repository"), lifeStyle: LifeStyle.PerLifetimeScope)
                .RegisterAssembly(domainServiceAssembly, m => m.Namespace != null && m.Namespace.StartsWith("Monitor.DomainService.Implement") && m.Name.EndsWith("DomainService"), lifeStyle: LifeStyle.PerLifetimeScope)
                .RegisterAssembly(cacheAssembly, m => m.Namespace != null && m.Namespace.StartsWith("Monitor.Cache.Implement") && m.Name.EndsWith("Cache"), lifeStyle: LifeStyle.PerLifetimeScope)
                .RegisterAssembly(userApplicationAssembly, m => m.Namespace != null && m.Namespace.StartsWith("Monitor.Application.Implement") && m.Name.EndsWith("Application"), lifeStyle: LifeStyle.PerLifetimeScope)
                 .RegisterAssembly(unitOfWorkAssembly, m => m.Namespace != null && m.Namespace.StartsWith("Monitor.UnitOfWork.Implement") && m.Name.EndsWith("UnitOfWork"), lifeStyle: LifeStyle.PerLifetimeScope)
                .AddScoped<IDataAccessFactory, DataAccessFactory>()
                .AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>()
                .AddSingleton<IBinarySerializer, DefaultBinarySerializer>()
                ;

            builder.RegisterAssemblyTypes(typeof(AutofacRegisterModule).GetTypeInfo().Assembly)
                .Where(
                    t =>
                        typeof(Controller).IsAssignableFrom(t) &&
                        t.Name.EndsWith("Controller", StringComparison.Ordinal)).PropertiesAutowired();

            base.Load(builder);
        }
    }
}