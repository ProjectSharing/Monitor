using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：OperateType.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：操作类型
    /// 创建标识：yjq 2017/10/9 14:44:34
    /// </summary>
    [DisplayName("操作类型")]
    public enum OperateType
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Add = 1,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Modify = 2,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 3
    }
}