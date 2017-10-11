using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JQCore.Dependency
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AutofacObjectContainer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/6 15:43:38
    /// </summary>
    public sealed class AutofacObjectContainer : IObjectContainer
    {
        private IContainer _container;
        private ContainerBuilder _containerBuilder;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="containerBuilder"></param>
        public AutofacObjectContainer(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder ?? new ContainerBuilder();
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="container"></param>
        public AutofacObjectContainer(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// 容器
        /// </summary>
        public IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (this)
                    {
                        if (_container == null)
                        {
                            _container = _containerBuilder.Build();
                        }
                    }
                }
                return _container;
            }
        }

        #region 注册

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="implementationType">实例类型</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="lifeStyle">生命周期</param>
        public void RegisterType(Type implementationType, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            var builder = _containerBuilder;
            var registrationBuilder = builder.RegisterType(implementationType).AsSelf();
            if (!string.IsNullOrWhiteSpace(serviceName))
            {
                registrationBuilder.Named(serviceName, implementationType);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="implementationType">实例类型</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="lifeStyle">生命周期</param>
        public void RegisterType(Type implementationType, Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            throw new NotSupportedException();
            //var builder = _containerBuilder;
            //var registrationBuilder = builder.RegisterType(implementationType).AsSelf();
            //if (!string.IsNullOrWhiteSpace(serviceName))
            //{
            //    registrationBuilder.Named(serviceName, implementationType);
            //}
            //registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors().SetLifeStyle(lifeStyle);
        }

        public void RegisterType<T>(string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            var builder = _containerBuilder;
            var registrationBuilder = builder.RegisterType<T>().AsSelf();
            if (!string.IsNullOrWhiteSpace(serviceName))
            {
                registrationBuilder.Named<T>(serviceName);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
        }

        public void RegisterType<T>(Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            throw new NotSupportedException();
            //var builder = _containerBuilder;
            //var registrationBuilder = builder.RegisterType<T>().AsSelf();
            //if (!string.IsNullOrWhiteSpace(serviceName))
            //{
            //    registrationBuilder.Named<T>(serviceName);
            //}
            //registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors().SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实例类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="life">生命周期</param>
        public void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            var builder = _containerBuilder;
            var registrationBuilder = builder.RegisterType(implementationType).As(serviceType);
            if (!string.IsNullOrWhiteSpace(serviceName))
            {
                registrationBuilder.Named(serviceName, implementationType);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实例类型</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        public void RegisterType(Type serviceType, Type implementationType, Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            throw new NotSupportedException();
            //var builder = _containerBuilder;
            //var registrationBuilder = builder.RegisterType(implementationType).As(serviceType);
            //if (!string.IsNullOrWhiteSpace(serviceName))
            //{
            //    registrationBuilder.Named(serviceName, implementationType);
            //}
            //registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors().SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        public void RegisterType<TService, TImplementer>(string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            var builder = _containerBuilder;
            var registrationBuilder = builder.RegisterType<TImplementer>().As<TService>();
            if (!string.IsNullOrWhiteSpace(serviceName))
            {
                registrationBuilder.Named<TService>(serviceName);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        public void RegisterType<TService, TImplementer>(Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            throw new NotSupportedException();
            //var builder = _containerBuilder;
            //var registrationBuilder = builder.RegisterType<TImplementer>().As<TService>();
            //if (!string.IsNullOrWhiteSpace(serviceName))
            //{
            //    registrationBuilder.Named<TService>(serviceName);
            //}
            //registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors().SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="instance">实例值</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            var builder = _containerBuilder;
            var registrationBuilder = builder.RegisterInstance(instance).As<TService>();
            if (serviceName != null)
            {
                registrationBuilder.Named<TService>(serviceName);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="instance">实例值</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            throw new NotSupportedException();
            //var builder = _containerBuilder;
            //var registrationBuilder = builder.RegisterInstance(instance).As<TService>();
            //if (serviceName != null)
            //{
            //    registrationBuilder.Named<TService>(serviceName);
            //}
            //registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors().SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 根据程序集注册
        /// </summary>
        /// <param name="assemblies">程序集</param>
        /// <param name="predicate">筛选条件</param>
        /// <param name="lifeStyle">生命周期</param>
        public void RegisterAssemblyTypes(Assembly assemblies, Func<Type, bool> predicate = null, LifeStyle lifeStyle = LifeStyle.PerLifetimeScope)
        {
            if (assemblies != null)
            {
                var builder = _containerBuilder;
                var registrationBuilder = builder.RegisterAssemblyTypes(assemblies);
                if (predicate != null)
                {
                    registrationBuilder.Where(predicate);
                }
                registrationBuilder.AsImplementedInterfaces().SetLifeStyle(lifeStyle);
            }
        }

        /// <summary>
        /// 根据程序集注册
        /// </summary>
        /// <param name="assemblies">程序集</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <param name="lifeStyle">生命周期</param>
        public void RegisterAssemblyTypes(Assembly assemblies, Type[] interceptTypeList, Func<Type, bool> predicate = null, LifeStyle lifeStyle = LifeStyle.PerLifetimeScope)
        {
            throw new NotSupportedException();
            //if (assemblies != null)
            //{
            //    var builder = _containerBuilder;
            //    var registrationBuilder = builder.RegisterAssemblyTypes(assemblies);
            //    if (predicate != null)
            //    {
            //        registrationBuilder.Where(predicate);
            //    }
            //    registrationBuilder.AsImplementedInterfaces().InterceptedBy(interceptTypeList).EnableInterfaceInterceptors().SetLifeStyle(lifeStyle);
            //}
        }

        #endregion 注册

        #region 解析获取

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>注册的服务类型</returns>
        public TService Resolve<TService>() where TService : class
        {
            return Container.Resolve<TService>();
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>注册的服务类型</returns>
        public object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>注册的服务类型</returns>
        public IEnumerable<TService> ResolveServices<TService>() where TService : class
        {
            return Container.Resolve<IEnumerable<TService>>();
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>注册的服务类型</returns>
        public IEnumerable<object> ResolveServices(Type serviceType)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="instance">服务类型默认实例</param>
        /// <returns>成功 则返回true</returns>
        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            return Container.TryResolve(out instance);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance"></param>
        /// <returns>成功 则返回true</returns>
        public bool TryResolve(Type serviceType, out object instance)
        {
            return Container.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="serviceName">服务名字</param>
        /// <returns>服务类型</returns>
        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return Container.ResolveNamed<TService>(serviceName);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceName">服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>服务类型</returns>
        public object ResolveNamed(string serviceName, Type serviceType)
        {
            return Container.ResolveNamed(serviceName, serviceType);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <param name="serviceName">服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">默认实例</param>
        /// <returns>成功 则返回true</returns>
        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return Container.TryResolveNamed(serviceName, serviceType, out instance);
        }

        #endregion 解析获取

        /// <summary>
        /// 开始一个作用域请求，与其它请求相互独立
        /// </summary>
        /// <returns>IIocScopeResolve</returns>
        public IIocScopeResolve BeginLeftScope()
        {
            return new AutofacLeftScope(Container.BeginLifetimeScope());
        }
    }
}