using System.ComponentModel;

namespace JQCore.Result
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AjaxState.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Ajax返回值枚举
    /// 创建标识：yjq 2017/9/11 20:58:03
    /// </summary>
    public enum AjaxState
    {
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed = 0,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 1,

        /// <summary>
        /// 未登录
        /// </summary>
        [Description("未登录")]
        NoLogin = 1000,

        /// <summary>
        /// 未授权
        /// </summary>
        [Description("未授权")]
        NoPerssion = 2000
    }
}