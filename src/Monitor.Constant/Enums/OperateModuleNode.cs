using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：OperateModuleNode.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：操作类型
    /// 创建标识：yjq 2017/9/25 10:54:14
    /// </summary>
    [DisplayName("操作类型")]
    public enum OperateModuleNode
    {
        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        Login = 1,

        /// <summary>
        /// 退出
        /// </summary>
        [Description("退出")]
        LogOut = 10,

        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Add = 20,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Edit = 30,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 40
    }
}