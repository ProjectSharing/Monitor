using Monitor.Constant;
using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RuntimeSqlPageQueryWhere.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/30 15:51:28
    /// </summary>
    public class RuntimeSqlPageQueryWhere : BasePageQueryWhere
    {
        /// <summary>
        /// 日志来源
        /// </summary>
        public Source? Source { get; set; }

        /// <summary>
        /// 所属项目
        /// </summary>
        public int? ProjectID { get; set; }

        /// <summary>
        /// 部署服务器ID
        /// </summary>
        public int? ServicerID { get; set; }

        /// <summary>
        /// 请求的GUID标识，同一个GUID表明是同一次请求
        /// </summary>
        public string RequestGuid { get; set; }

        /// <summary>
        /// 最小消耗时间
        /// </summary>
        public double? MinTimeElapsed { get; set; }

        /// <summary>
        /// 最大消耗时间
        /// </summary>
        public double? MaxTimeElapsed { get; set; }

        /// <summary>
        /// 开始执行时间
        /// </summary>
        public DateTime? ExecuteTimeStart { get; set; }

        /// <summary>
        /// 截止执行时间
        /// </summary>
        public DateTime? ExecuteTimeEnd { get; set; }

        /// <summary>
        /// 截止执行时间值(查询时使用)
        /// </summary>
        public DateTime? ExecuteTimeEndValue
        {
            get
            {
                if (ExecuteTimeEnd == null) return null;
                else
                {
                    return ExecuteTimeEnd.Value.AddDays(1);
                }
            }
        }
    }
}