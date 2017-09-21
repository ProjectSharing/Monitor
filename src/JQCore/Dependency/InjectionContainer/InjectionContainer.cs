using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace JQCore.Dependency
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：InjectionContainer.cs
    /// 类属性：局部类（非静态）
    /// 类功能描述：Injection容器
    /// 创建标识：yjq 2017/9/4 11:28:00
    /// </summary>
    internal sealed class InjectionContainer : IObjectContainer
    {
        private readonly IServiceProvider _serviceProvider;

        public InjectionContainer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IIocScopeResolve BeginLeftScope()
        {
            return new InjectionScope(_serviceProvider.CreateScope());
        }

        public TService Resolve<TService>() where TService : class
        {
            return _serviceProvider.GetService<TService>();
        }

        public object Resolve(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
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
            return _serviceProvider.GetServices<TService>();
        }

        public IEnumerable<object> ResolveServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
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