using System.ComponentModel;

namespace Monitor.Constant
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：StatisticsValueType.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：统计值类型枚举
    /// 创建标识：yjq 2017/12/2 13:29:18
    /// </summary>
    public enum StatisticsValueType
    {
        /// <summary>
        /// 最大值
        /// </summary>
        [Description("最大值")]
        MaxValue = 1,

        /// <summary>
        /// 平均值
        /// </summary>
        [Description("平均值")]
        AverageValue = 2,

        /// <summary>
        /// 最小值
        /// </summary>
        [Description("最小值")]
        MinValue = 3,

        /// <summary>
        /// 失败次数
        /// </summary>
        [Description("失败次数")]
        FailCount = 4,

        /// <summary>
        /// 执行次数
        /// </summary>
        [Description("执行次数")]
        ExecutedCount = 5
    }
}