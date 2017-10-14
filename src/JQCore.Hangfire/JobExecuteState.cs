using System.ComponentModel;

namespace JQCore.Hangfire
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：JobExecuteState.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：任务执行状态
    /// 创建标识：yjq 2017/10/14 16:23:11
    /// </summary>
    public enum JobExecuteState
    {
        /// <summary>
        /// 任何状态
        /// </summary>
        [Description("任何状态")]
        OnAnyFinishedState = 0,

        /// <summary>
        /// 执行成功
        /// </summary>
        [Description("执行成功")]
        OnlyOnSucceededState = 1
    }
}