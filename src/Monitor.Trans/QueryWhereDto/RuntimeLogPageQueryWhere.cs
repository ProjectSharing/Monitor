using Monitor.Constant;
using System;

namespace Monitor.Trans
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RuntimeLogPageQueryWhere.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/10 11:44:55
    /// </summary>
    public class RuntimeLogPageQueryWhere : BasePageQueryWhere
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel? LogLevel { get; set; }

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


        /// <summary>
        /// 判断是否需要警告
        /// </summary>
        /// <returns>true表示需要提示</returns>
        public bool IsNeedWarning()
        {
            if (LogLevel == null) return false;
            return LogLevel.Value == Constant.LogLevel.Error || LogLevel.Value == Constant.LogLevel.Warn || LogLevel.Value == Constant.LogLevel.Fatal;
        }
    }
}