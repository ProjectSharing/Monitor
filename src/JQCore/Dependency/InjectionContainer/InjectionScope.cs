using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace JQCore.Dependency
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：InjectionScope.cs
    /// 类属性：局部类（非静态）
    /// 类功能描述：解析作用域
    /// 创建标识：yjq 2017/9/4 11:19:06
    /// </summary>
    internal sealed class InjectionScope : IIocScopeResolve
    {
        private readonly IServiceScope _serviceScope;

        public InjectionScope(IServiceScope serviceScope)
        {
            _serviceScope = serviceScope;
        }

        public void Dispose()
        {
            _serviceScope?.Dispose();
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>注册的服务类型</returns>
        public TService Resolve<TService>() where TService : class
        {
            return _serviceScope.ServiceProvider.GetService<TService>();
        }

        public object Resolve(Type serviceType)
        {
            return _serviceScope.ServiceProvider.GetService(serviceType);
        }

        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            throw new NotImplementedException();
        }

        public object ResolveNamed(string serviceName, Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TService> ResolveServices<TService>() where TService : class
        {
            return _serviceScope.ServiceProvider.GetServices<TService>();
        }

        public IEnumerable<object> ResolveServices(Type serviceType)
        {
            return _serviceScope.ServiceProvider.GetServices(serviceType);
        }

        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            throw new NotImplementedException();
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            throw new NotImplementedException();
        }

        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            throw new NotImplementedException();
        }
    }
}