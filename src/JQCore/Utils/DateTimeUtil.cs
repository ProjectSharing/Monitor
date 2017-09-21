using System;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DateTimeUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：时间帮助类
    /// 创建标识：yjq 2017/9/5 14:17:43
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <returns>当前时间的时间戳</returns>
        public static long GetTimeSpanNow()
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(DateTime.UtcNow - startTime).TotalMilliseconds;
        }

        /// <summary>
        /// 将时间转为时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns>时间戳</returns>
        public static long GetTimeSpan(this DateTime time)
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return (long)(time - startTime).TotalMilliseconds;
        }

        /// <summary>
        /// 将时间戳转为时间
        /// </summary>
        /// <param name="totalSeconds"></param>
        /// <returns>时间</returns>
        public static DateTime ToTime(this long totalMilliseconds)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return start.AddMilliseconds(totalMilliseconds);
        }

        /// <summary>
        /// 获取当天的最小时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>当天的最小时间</returns>
        public static DateTime GetTodayMinTime(this DateTime dateTime)
        {
            return DateTime.Parse(dateTime.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// 获取当天的明天的最小时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>当天的明天的最小时间</returns>
        public static DateTime GetTommowMinTime(this DateTime dateTime)
        {
            return DateTime.Parse(dateTime.AddDays(1).ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// 将时间转为UTC时间
        /// </summary>
        /// <param name="dateTime">要转换的时间</param>
        /// <returns>UTC时间</returns>
        public static DateTime ToUniversalTime(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
            {
                return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
            }
            else if (dateTime == DateTime.MaxValue)
            {
                return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
            }
            else
            {
                return dateTime.ToUniversalTime();
            }
        }
    }
}