using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：UserState.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：用户状态枚举
    /// 创建标识：yjq 2017/9/25 10:21:36
    /// </summary>
    [DisplayName("用户状态")]
    public enum UserState
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,

        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 10,

        /// <summary>
        /// 注销
        /// </summary>
        [Description("注销")]
        Cancel = 20
    }
}