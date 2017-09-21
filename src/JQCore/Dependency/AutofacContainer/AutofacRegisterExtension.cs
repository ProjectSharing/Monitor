using Autofac;
using Autofac.Builder;
using System;
using System.Linq;
using System.Reflection;

namespace JQCore.Dependency
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AutofacRegisterExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：autofac注册扩展方法
    /// 创建标识：yjq 2017/9/6 16:09:50
    /// </summary>
    public static class AutofacRegisterExtension
    {
        /// <summary>
        /// 注册单例
        /// </summary>
        /// <typeparam name="TService">接口类</typeparam>
        /// <typeparam name="TImplementer">实现类</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder AddSingleton<TService, TImplementer>(this ContainerBuilder builder)
        {
            builder.RegisterType<TImplementer>().As<TService>().SingleInstance();
            return builder;
        }

        /// <summary>
        /// 生命周期里同一个实例
        /// </summary>
        /// <typeparam name="TService">接口类</typeparam>
        /// <typeparam name="TImplementer">实现类</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder AddScoped<TService, TImplementer>(this ContainerBuilder builder)
        {
            builder.RegisterType<TImplementer>().As<TService>().InstancePerLifetimeScope();
            return builder;
        }

        /// <summary>
        /// 默认
        /// </summary>
        /// <typeparam name="TService">接口类</typeparam>
        /// <typeparam name="TImplementer">实现类</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder AddTransient<TService, TImplementer>(this ContainerBuilder builder)
        {
            builder.RegisterType<TImplementer>().As<TService>().InstancePerDependency();
            return builder;
        }

        /// <summary>
        /// 程序集注册
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblies"></param>
        /// <param name="predicate"></param>
        /// <param name="lifeStyle"></param>
        /// <returns></returns>
        public static ContainerBuilder RegisterAssembly(this ContainerBuilder builder, Assembly assemblies, Func<Type, bool> predicate = null, LifeStyle lifeStyle = LifeStyle.PerLifetimeScope)
        {
            if (assemblies != null)
            {
                var registrationBuilder = builder.RegisterAssemblyTypes(assemblies);
                if (predicate != null)
                {
                    registrationBuilder.Where(predicate);
                }
                registrationBuilder.AsImplementedInterfaces().SetLifeStyle(lifeStyle);
            }
            return builder;
        }

        /// <summary>
        /// 设置生命周期
        /// </summary>
        /// <typeparam name="TImplementer"></typeparam>
        /// <typeparam name="TActivatorData"></typeparam>
        /// <typeparam name="TRegistrationStyle"></typeparam>
        /// <param name="registrationBuilder"></param>
        /// <param name="lifeStyle"></param>
        public static void SetLifeStyle<TImplementer, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TImplementer, TActivatorData, TRegistrationStyle> registrationBuilder, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            switch (lifeStyle)
            {
                case LifeStyle.Transient:
                    registrationBuilder.InstancePerDependency();
                    break;

                case LifeStyle.Singleton:
                    registrationBuilder.SingleInstance();
                    break;

                case LifeStyle.PerLifetimeScope:
                    registrationBuilder.InstancePerLifetimeScope();
                    break;

                default:
                    break;
            }
        }
    }
}