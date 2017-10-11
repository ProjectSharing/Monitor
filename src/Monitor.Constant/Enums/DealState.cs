using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DealState.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：处理状态枚举
    /// 创建标识：yjq 2017/10/9 16:25:33
    /// </summary>
    [DisplayName("处理状态")]
    public enum DealState
    {
        /// <summary>
        /// 待处理
        /// </summary>
        [Description("待处理")]
        WaitDeal = 1,

        /// <summary>
        /// 已处理
        /// </summary>
        [Description("已处理")]
        Dealed = 2,

        /// <summary>
        /// 不处理
        /// </summary>
        [Description("不处理")]
        NotDeal = 3
    }
}