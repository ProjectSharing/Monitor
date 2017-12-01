using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：OperateModule.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：操作模块
    /// 创建标识：yjq 2017/9/25 10:53:28
    /// </summary>
    [DisplayName("操作模块")]
    public enum OperateModule
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]
        User = 1,

        /// <summary>
        /// 服务器
        /// </summary>
        [Description("服务器")]
        Servicer = 2,

        /// <summary>
        /// 系统配置
        /// </summary>
        [Description("系统配置")]
        SysConfig = 3,

        /// <summary>
        /// 项目信息
        /// </summary>
        [Description("项目信息")]
        Project = 4,

        /// <summary>
        /// 数据库信息
        /// </summary>
        [Description("数据库信息")]
        Database = 5
    }
}