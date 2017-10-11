using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：NoticeState.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：通知状态枚举
    /// 创建标识：yjq 2017/10/9 16:22:16
    /// </summary>
    [DisplayName("通知状态")]
    public enum NoticeState
    {
        /// <summary>
        /// 未通知
        /// </summary>
        [Description("未通知")]
        WaitNotice = 1,

        /// <summary>
        /// 已通知
        /// </summary>
        [Description("已通知")]
        Noticed = 2,

        /// <summary>
        /// 通知失败
        /// </summary>
        [Description("通知失败")]
        NoticedFaild = 3
    }
}