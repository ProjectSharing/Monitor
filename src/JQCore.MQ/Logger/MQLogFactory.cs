using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JQCore.MQ.Logger
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MQLogFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/8 15:54:30
    /// </summary>
    public sealed class MQLogFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {
            // throw new NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new MQLogger();
        }

        public void Dispose()
        {

        }
    }
}
