using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：LogLevel.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：日志级别枚举
    /// 创建标识：yjq 2017/10/2 20:03:47
    /// </summary>
    [DisplayName("日志类型")]
    public enum LogLevel
    {
        /// <summary>
        /// 调试日志
        /// </summary>
        [Description("调试日志")]
        Debug = 1,

        /// <summary>
        /// 普通日志
        /// </summary>
        [Description("普通日志")]
        Info = 2,

        /// <summary>
        /// 警告日志
        /// </summary>
        [Description("警告日志")]
        Warn = 3,

        /// <summary>
        /// 错误日志
        /// </summary>
        [Description("错误日志")]
        Error = 4,

        /// <summary>
        /// 严重错误日志
        /// </summary>
        [Description("严重错误日志")]
        Fatal = 5
    }
}