using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DimensionType.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：统计维度类型
    /// 创建标识：yjq 2017/12/2 13:32:47
    /// </summary>
    public enum DimensionType
    {
        /// <summary>
        /// 项目
        /// </summary>
        [Description("项目")]
        Project = 1,

        /// <summary>
        /// 数据库
        /// </summary>
        [Description("数据库")]
        Database = 2
    }
}