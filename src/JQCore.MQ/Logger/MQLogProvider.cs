using Microsoft.Extensions.Logging;

namespace JQCore.MQ.Logger
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MQLogProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/8 15:58:45
    /// </summary>
    public sealed class MQLogProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new MQLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }
}