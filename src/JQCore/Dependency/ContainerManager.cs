using Autofac;
using System;

namespace JQCore.Dependency
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ContainerManager.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：容器管理类
    /// 创建标识：yjq 2017/9/4 11:36:22
    /// </summary>
    public class ContainerManager
    {
        private ContainerManager()
        {
        }

        /// <summary>
        /// 初始化容器管理类
        /// </summary>
        static ContainerManager()
        {
            Instance = new ContainerManager();
        }

        /// <summary>
        /// 当前容器
        /// </summary>
        public IObjectContainer Container { get; private set; }

        /// <summary>
        /// 当前容器实例
        /// </summary>
        public static ContainerManager Instance { get; private set; }

        /// <summary>
        /// 使用Injection
        /// </summary>
        /// <param name="provider">服务提供者</param>
        /// <returns></returns>
        public static ContainerManager UseInjectionContainer(IServiceProvider provider)
        {
            return SetContainer(new InjectionContainer(provider));
        }

        /// <summary>
        /// 使用autofac作为容器
        /// </summary>
        /// <param name="container">autofac容器</param>
        /// <returns></returns>
        public static ContainerManager UseAutofacContainer(IContainer container)
        {
            return SetContainer(new AutofacObjectContainer(container));
        }

        private static ContainerManager SetContainer(IObjectContainer container)
        {
            Instance.Container = container;
            return Instance;
        }

        #region 解析获取

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>注册的服务类型</returns>
        public static TService Resolve<TService>() where TService : class
        {
            return Instance.Container.Resolve<TService>();
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>注册的服务类型</returns>
        public static object Resolve(Type serviceType)
        {
            return Instance.Container.Resolve(serviceType);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="instance">服务类型默认实例</param>
        /// <returns>成功 则返回true</returns>
        public static bool TryResolve<TService>(out TService instance) where TService : class
        {
            return Instance.Container.TryResolve(out instance);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance"></param>
        /// <returns>成功 则返回true</returns>
        public static bool TryResolve(Type serviceType, out object instance)
        {
            return Instance.Container.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="serviceName">服务名字</param>
        /// <returns>服务类型</returns>
        public static TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return Instance.Container.ResolveNamed<TService>(serviceName);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceName">服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>服务类型</returns>
        public static object ResolveNamed(string serviceName, Type serviceType)
        {
            return Instance.Container.ResolveNamed(serviceName, serviceType);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <param name="serviceName">服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">默认实例</param>
        /// <returns>成功 则返回true</returns>
        public static bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return Instance.Container.TryResolveNamed(serviceName, serviceType, out instance);
        }

        #endregion 解析获取

        /// <summary>
        /// 开始一个作用域请求，与其它请求相互独立
        /// </summary>
        /// <returns>IIocScopeResolve</returns>
        public static IIocScopeResolve BeginLeftScope()
        {
            return Instance.Container.BeginLeftScope();
        }
    }
}